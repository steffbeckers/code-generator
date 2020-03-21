import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Document } from 'src/app/shared/models/Document';

// Services
import { DocumentService } from 'src/app/shared/services/DocumentService';

@Component({
  selector: 'app-document-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class DocumentDetailComponent implements OnInit {
  // Document
  public document: Document;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private documentService: DocumentService
  ) {
    this.document = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getDocument(routeParams.id);
    });
  }

  private getDocument(id: string): void {
    this.documentService.getDocument(id).subscribe(
      (document: Document) => {
        this.document = document;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Document could not be found.');
          this.router.navigateByUrl('/documents');
        }
      }
    );
  }

  public deleteDocument(): void {
    // Validate
    if (!this.document && !this.document.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete document: ' + this.document.id + '?')) {
      this.documentService.deleteDocument(this.document.id).subscribe(
        () => {
          this.router.navigateByUrl('/documents');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Document could not be found.');
            this.router.navigateByUrl('/documents');
          }
        }
      );
    }
  }
}

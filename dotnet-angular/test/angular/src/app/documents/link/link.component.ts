import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Document } from 'src/app/shared/models/Document';

// Services
import { DocumentService } from 'src/app/shared/services/DocumentService';

@Component({
  selector: 'app-document-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class DocumentLinkComponent implements OnInit {
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
}

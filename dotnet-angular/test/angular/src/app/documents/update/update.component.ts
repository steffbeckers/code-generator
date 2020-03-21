import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Document } from 'src/app/shared/models/Document';

// Services
import { DocumentService } from 'src/app/shared/services/DocumentService';

@Component({
  selector: 'app-document-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class DocumentUpdateComponent implements OnInit {
  // Document
  public document: Document;
  public documentForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private documentService: DocumentService
  ) {
    this.document = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.documentForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      displayName: [''],
      description: [''],
      path: [''],
      uRL: [''],
      mimeType: [''],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getDocument(routeParams.id);
    });
  }

  private getDocument(id: string): void {
    this.documentService.getDocument(id).subscribe(
      (document: Document) => {
        this.document = document;
        this.documentForm.patchValue(this.document);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Document could not be found.');
          this.router.navigateByUrl('/documents');
        }
      }
    );
  }

  public updateDocument(andClose: boolean = false): void {
    // Validate
    if (this.documentForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.documentForm.pristine && andClose) {
      this.router.navigateByUrl('/documents/' + this.document.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.documentService.updateDocument(this.documentForm.value).subscribe(
      (document: Document) => {
        if (andClose) {
          this.router.navigateByUrl('/documents/' + document.id);
        }

        this.document = document;
        this.documentForm.patchValue(this.document);
      },
      null,
      () => {
        this.updating = false;
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

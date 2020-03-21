import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Document } from 'src/app/shared/models/Document';

// Services
import { DocumentService } from 'src/app/shared/services/DocumentService';

@Component({
  selector: 'app-document-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class DocumentCreateComponent implements OnInit {
  // Document
  public documentForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private documentService: DocumentService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.documentForm = this.fb.group({
      name: ['', Validators.required],
      displayName: [''],
      description: [''],
      path: [''],
      uRL: [''],
      mimeType: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.documentForm.patchValue(queryParams);
    });
  }

  public createDocument(): void {
    // Validate
    if (this.documentForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.documentService.createDocument(this.documentForm.value).subscribe(
      (document: Document) => {
        this.creating = false;

        this.router.navigateByUrl('/documents/' + document.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

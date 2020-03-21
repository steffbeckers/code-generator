import { Component, OnInit } from '@angular/core';

// Models
import { Document } from 'src/app/shared/models/Document';

// Services
import { DocumentService } from 'src/app/shared/services/DocumentService';

@Component({
  selector: 'app-documents-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class DocumentsListComponent implements OnInit {
  public documents: Document[];

  constructor(private documentService: DocumentService) {
    this.documents = null;
  }

  ngOnInit(): void {
    this.getDocuments();
  }

  private getDocuments(): void {
    this.documentService.getDocuments().subscribe(
      (documents: Document[]) => {
        this.documents = documents;
      }
    );
  }
}

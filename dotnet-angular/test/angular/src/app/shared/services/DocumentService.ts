import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Document } from 'src/app/shared/models/Document';

@Injectable({ providedIn: 'root' })
export class DocumentService {
  constructor(private http: HttpClient) {}

  // GET: api/documents
  // Retrieves all documents.
  public getDocuments(): Observable<Document[]> {
    return this.http.get<Document[]>(`${environment.api}/documents`);
  }

  // GET: api/documents/{id}
  // Retrieves a specific document.
  public getDocument(document: Document | string): Observable<Document> {
    const id = typeof document === 'string' ? document : (document as Document).id;
    return this.http.get<Document>(`${environment.api}/documents/${id}`);
  }

  // POST: api/documents
  // Creates a new document.
  public createDocument(document: Document): Observable<Document> {
    return this.http.post<Document>(`${environment.api}/documents`, document);
  }

  // PUT: api/documents/{id}
  // Updates a specific document.
  public updateDocument(document: Document): Observable<Document> {
    return this.http.put<Document>(`${environment.api}/documents/${document.id}`, document);
  }

  // PUT: api/Documents/{documentId}/resumes/{resumeId}/link
  // Links a specific resume to document.
  public linkResumeToDocument(documentId: string, resumeId: string): Observable<Document> {
    return this.http.put<Document>(`${environment.api}/documents/${documentId}/resumes/${resumeId}/link`, null);
  }

  // PUT: api/Documents/{documentId}/resumes/{resumeId}/unlink
  // Unlinks a specific resume from document.
  public unlinkResumeFromDocument(documentId: string, resumeId: string): Observable<Document> {
    return this.http.put<Document>(`${environment.api}/documents/${documentId}/resumes/${resumeId}/unlink`, null);
  }

  // DELETE: api/documents/{id}
  // Deletes a specific document.
  public deleteDocument(document: Document | string): Observable<Document> {
    const id = typeof document === 'string' ? document : (document as Document).id;
    return this.http.delete<Document>(`${environment.api}/documents/${id}`);
  }
}

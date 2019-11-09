import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Note } from 'src/app/shared/models/Note';

@Injectable({ providedIn: 'root' })
export class NoteService {
  constructor(private http: HttpClient) {}

  // GET: api/notes
  // Retrieves all notes.
  public getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>(`${environment.api}/notes`);
  }

  // GET: api/notes/{id}
  // Retrieves a specific note.
  public getNote(note: Note | string): Observable<Note> {
    const id = typeof note === 'string' ? note : (note as Note).id;
    return this.http.get<Note>(`${environment.api}/notes/${id}`);
  }

  // POST: api/notes
  // Creates a new note.
  public createNote(note: Note): Observable<Note> {
    return this.http.post<Note>(`${environment.api}/notes`, note);
  }

  // PUT: api/notes/{id}
  // Updates a specific note.
  public updateNote(note: Note): Observable<Note> {
    return this.http.put<Note>(`${environment.api}/notes/${note.id}`, note);
  }

  // TODO
  // PUT: api/Notes/{noteId}/Accounts/{accountId}/Link
  
  // TODO
  // DELETE: api/Notes/{noteId}/Accounts/{accountId}/Link

  // DELETE: api/notes/{id}
  // Deletes a specific note.
  public deleteNote(note: Note | string): Observable<Note> {
    const id = typeof note === 'string' ? note : (note as Note).id;
    return this.http.delete<Note>(`${environment.api}/notes/${id}`);
  }
}

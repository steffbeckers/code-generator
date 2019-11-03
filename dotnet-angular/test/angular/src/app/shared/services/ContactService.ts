import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Contact } from 'src/app/shared/models/Contact';

@Injectable({ providedIn: 'root' })
export class ContactService {
  constructor(private http: HttpClient) {}

  // GET: api/contacts
  // Retrieves all contacts.
  public getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${environment.api}/contacts`);
  }

  // GET: api/contacts/{id}
  // Retrieves a specific contact.
  public getContact(contact: Contact | string): Observable<Contact> {
    const id = typeof contact === 'string' ? contact : (contact as Contact).id;
    return this.http.get<Contact>(`${environment.api}/contacts/${id}`);
  }

  // POST: api/contacts
  // Creates a new contact.
  public createContact(contact: Contact): Observable<Contact> {
    return this.http.post<Contact>(`${environment.api}/contacts`, contact);
  }

  // PUT: api/contacts/{id}
  // Updates a specific contact.
  public updateContact(contact: Contact): Observable<Contact> {
    return this.http.put<Contact>(`${environment.api}/contacts/${contact.id}`, contact);
  }

  // DELETE: api/contacts/{id}
  // Deletes a specific contact.
  public deleteContact(contact: Contact | string): Observable<Contact> {
    const id = typeof contact === 'string' ? contact : (contact as Contact).id;
    return this.http.delete<Contact>(`${environment.api}/contacts/${id}`);
  }
}

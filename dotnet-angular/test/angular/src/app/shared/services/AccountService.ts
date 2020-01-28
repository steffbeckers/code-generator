import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Account } from 'src/app/shared/models/Account';

@Injectable({ providedIn: 'root' })
export class AccountService {
  constructor(private http: HttpClient) {}

  // GET: api/accounts
  // Retrieves all accounts.
  public getAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(`${environment.api}/accounts`);
  }

  // GET: api/accounts/{id}
  // Retrieves a specific account.
  public getAccount(account: Account | string): Observable<Account> {
    const id = typeof account === 'string' ? account : (account as Account).id;
    return this.http.get<Account>(`${environment.api}/accounts/${id}`);
  }

  // POST: api/accounts
  // Creates a new account.
  public createAccount(account: Account): Observable<Account> {
    return this.http.post<Account>(`${environment.api}/accounts`, account);
  }

  // PUT: api/accounts/{id}
  // Updates a specific account.
  public updateAccount(account: Account): Observable<Account> {
    return this.http.put<Account>(`${environment.api}/accounts/${account.id}`, account);
  }

  // PUT: api/Accounts/{accountId}/notes/{noteId}/link
  // Links a specific note to account.
  public linkNoteToAccount(accountId: string, noteId: string): Observable<Account> {
    return this.http.put<Account>(`${environment.api}/accounts/${accountId}/notes/${noteId}/link`, null);
  }

  // PUT: api/Accounts/{accountId}/notes/{noteId}/unlink
  // Unlinks a specific note from account.
  public unlinkNoteFromAccount(accountId: string, noteId: string): Observable<Account> {
    return this.http.put<Account>(`${environment.api}/accounts/${accountId}/notes/${noteId}/unlink`);
  }

  // DELETE: api/accounts/{id}
  // Deletes a specific account.
  public deleteAccount(account: Account | string): Observable<Account> {
    const id = typeof account === 'string' ? account : (account as Account).id;
    return this.http.delete<Account>(`${environment.api}/accounts/${id}`);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from 'src/app/shared/models/account.model';
import { Response } from 'src/app/shared/models/response.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class AccountsService {
  constructor(private http: HttpClient) {}

  getAccounts(include: string = null): Observable<Response> {
    let params: { include?: string } = {};
    if (include) {
      params.include = include;
    }

    return this.http.get<Response>(`${environment.api}/accounts`, {
      params,
    });
  }

  getAccountById(id: string, include: string = null): Observable<Response> {
    let params: { include?: string } = {};
    if (include) {
      params.include = include;
    }

    return this.http.get<Response>(`${environment.api}/accounts/${id}`, {
      params,
    });
  }

  updateAccount(account: Account): Observable<Response> {
    return this.http.put<Response>(
      `${environment.api}/accounts/${account.id}`,
      account
    );
  }

  deleteAccount(account: Account): Observable<Response> {
    return this.http.delete<Response>(
      `${environment.api}/accounts/${account.id}`
    );
  }
}

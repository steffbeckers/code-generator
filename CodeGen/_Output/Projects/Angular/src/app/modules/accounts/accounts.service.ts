import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class AccountsService {
  constructor(private http: HttpClient) {}

  getAccounts(include: string = null) {
    let params: { include?: string } = {};
    if (include) {
      params.include = include;
    }

    return this.http.get(`${environment.api}/accounts`, {
      params,
    });
  }

  getAccountById(id: string, include: string = null) {
    let params: { include?: string } = {};
    if (include) {
      params.include = include;
    }

    return this.http.get(`${environment.api}/accounts/${id}`, {
      params,
    });
  }
}

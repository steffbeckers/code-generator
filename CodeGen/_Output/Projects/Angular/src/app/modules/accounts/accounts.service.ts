import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class AccountsService {
  constructor(private http: HttpClient) {}

  getAccounts(include?: string) {
    let params: { include?: string } = {};
    if (include) {
      params.include = include;
    }

    return this.http.get(`${environment.api}/accounts`, {
      params,
    });
  }
}

import { Injectable } from './edit-__entityName@dasherize__/node_modules/@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';

// RxJS
import { of } from 'rxjs';

// Models
import { Address } from '../shared/models/address';

@Injectable()
export class AddressService {
  constructor(private http: HttpClient) {}

  // Create
  public create(address: Address) {
    return this.http.post(environment.api + '/addresses', address);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + '/addresses');
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/addresses/${id}`);
  }

  public getByName(name: string) {
    if (name === '') {
      return of([]);
    }

    let params = new HttpParams();
    params.set('name', name);

    return this.http.get(environment.api + '/addresses', { params });
  }

  // Update
  public update(id: string, address: Address) {
    return this.http.put(environment.api + `/addresses/${id}`, address);
  }

  public patch(id: string, address: Address) {
    return this.http.patch(environment.api + `/addresses/${id}`, address);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/addresses/${id}`);
  }
}

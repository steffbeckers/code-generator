import { Injectable } from './edit-__entityName@dasherize__/node_modules/@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';

// RxJS
import { of } from 'rxjs';

// Models
import { Country } from '../shared/models/country';

@Injectable()
export class CountryService {
  constructor(private http: HttpClient) {}

  // Create
  public create(country: Country) {
    return this.http.post(environment.api + '/countries', country);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + '/countries');
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/countries/${id}`);
  }

  public getByName(name: string) {
    if (name === '') {
      return of([]);
    }

    let params = new HttpParams();
    params.set('name', name);

    return this.http.get(environment.api + '/countries', { params });
  }

  // Update
  public update(id: string, country: Country) {
    return this.http.put(environment.api + `/countries/${id}`, country);
  }

  public patch(id: string, country: Country) {
    return this.http.patch(environment.api + `/countries/${id}`, country);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/countries/${id}`);
  }
}

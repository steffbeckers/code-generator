import { Injectable } from './edit-__entityName@dasherize__/node_modules/@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '@env/environment';

// RxJS
import { of } from 'rxjs';

// Models
import { User } from '../shared/models/user';

@Injectable()
export class UserService {
  constructor(private http: HttpClient) {}

  // Create
  public create(user: User) {
    return this.http.post(environment.api + '/users', user);
  }

  // Read
  public getAll() {
    return this.http.get(environment.api + '/users');
  }

  public getById(id: string) {
    return this.http.get(environment.api + `/users/${id}`);
  }

  public getByName(name: string) {
    if (name === '') {
      return of([]);
    }

    let params = new HttpParams();
    params.set('name', name);

    return this.http.get(environment.api + '/users', { params });
  }

  // Update
  public update(id: string, user: User) {
    return this.http.put(environment.api + `/users/${id}`, user);
  }

  public patch(id: string, user: User) {
    return this.http.patch(environment.api + `/users/${id}`, user);
  }

  // Delete
  public delete(id: string) {
    return this.http.delete(environment.api + `/users/${id}`);
  }
}

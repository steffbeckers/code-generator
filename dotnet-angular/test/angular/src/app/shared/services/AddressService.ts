import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Address } from 'src/app/shared/models/Address';

@Injectable({ providedIn: 'root' })
export class AddressService {
  constructor(private http: HttpClient) {}

  // GET: api/addresses
  // Retrieves all addresses.
  public getAddresses(): Observable<Address[]> {
    return this.http.get<Address[]>(`${environment.api}/addresses`);
  }

  // GET: api/addresses/{id}
  // Retrieves a specific address.
  public getAddress(address: Address | string): Observable<Address> {
    const id = typeof address === 'string' ? address : (address as Address).id;
    return this.http.get<Address>(`${environment.api}/addresses/${id}`);
  }

  // POST: api/addresses
  // Creates a new address.
  public createAddress(address: Address): Observable<Address> {
    return this.http.post<Address>(`${environment.api}/addresses`, address);
  }

  // PUT: api/addresses/{id}
  // Updates a specific address.
  public updateAddress(address: Address): Observable<Address> {
    return this.http.put<Address>(`${environment.api}/addresses/${address.id}`, address);
  }

  // DELETE: api/addresses/{id}
  // Deletes a specific address.
  public deleteAddress(address: Address | string): Observable<Address> {
    const id = typeof address === 'string' ? address : (address as Address).id;
    return this.http.delete<Address>(`${environment.api}/addresses/${id}`);
  }
}

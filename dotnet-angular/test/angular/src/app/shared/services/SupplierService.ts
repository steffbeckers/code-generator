import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

@Injectable({ providedIn: 'root' })
export class SupplierService {
  constructor(private http: HttpClient) {}

  // GET: api/suppliers
  // Retrieves all suppliers.
  public getSuppliers(): Observable<Supplier[]> {
    return this.http.get<Supplier[]>(`${environment.api}/suppliers`);
  }

  // GET: api/suppliers/{id}
  // Retrieves a specific supplier.
  public getSupplier(supplier: Supplier | string): Observable<Supplier> {
    const id = typeof supplier === 'string' ? supplier : (supplier as Supplier).id;
    return this.http.get<Supplier>(`${environment.api}/suppliers/${id}`);
  }

  // POST: api/suppliers
  // Creates a new supplier.
  public createSupplier(supplier: Supplier): Observable<Supplier> {
    return this.http.post<Supplier>(`${environment.api}/suppliers`, supplier);
  }

  // PUT: api/suppliers/{id}
  // Updates a specific supplier.
  public updateSupplier(supplier: Supplier): Observable<Supplier> {
    return this.http.put<Supplier>(`${environment.api}/suppliers/${supplier.id}`, supplier);
  }

  // PUT: api/Suppliers/{supplierId}/products/{productId}/link
  // Links a specific product to supplier.
  public linkProductToSupplier(supplierId: string, productId: string): Observable<Supplier> {
    return this.http.put<Supplier>(`${environment.api}/suppliers/${supplierId}/products/${productId}/link`, null);
  }

  // PUT: api/Suppliers/{supplierId}/products/{productId}/unlink
  // Unlinks a specific product from supplier.
  public unlinkProductFromSupplier(supplierId: string, productId: string): Observable<Supplier> {
    return this.http.put<Supplier>(`${environment.api}/suppliers/${supplierId}/products/${productId}/unlink`);
  }

  // DELETE: api/suppliers/{id}
  // Deletes a specific supplier.
  public deleteSupplier(supplier: Supplier | string): Observable<Supplier> {
    const id = typeof supplier === 'string' ? supplier : (supplier as Supplier).id;
    return this.http.delete<Supplier>(`${environment.api}/suppliers/${id}`);
  }
}

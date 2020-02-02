import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Product } from 'src/app/shared/models/Product';

@Injectable({ providedIn: 'root' })
export class ProductService {
  constructor(private http: HttpClient) {}

  // GET: api/products
  // Retrieves all products.
  public getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${environment.api}/products`);
  }

  // GET: api/products/{id}
  // Retrieves a specific product.
  public getProduct(product: Product | string): Observable<Product> {
    const id = typeof product === 'string' ? product : (product as Product).id;
    return this.http.get<Product>(`${environment.api}/products/${id}`);
  }

  // POST: api/products
  // Creates a new product.
  public createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${environment.api}/products`, product);
  }

  // PUT: api/products/{id}
  // Updates a specific product.
  public updateProduct(product: Product): Observable<Product> {
    return this.http.put<Product>(`${environment.api}/products/${product.id}`, product);
  }

  // PUT: api/Products/{productId}/suppliers/{supplierId}/link
  // Links a specific supplier to product.
  public linkSupplierToProduct(productId: string, supplierId: string): Observable<Product> {
    return this.http.put<Product>(`${environment.api}/products/${productId}/suppliers/${supplierId}/link`, null);
  }

  // PUT: api/Products/{productId}/suppliers/{supplierId}/unlink
  // Unlinks a specific supplier from product.
  public unlinkSupplierFromProduct(productId: string, supplierId: string): Observable<Product> {
    return this.http.put<Product>(`${environment.api}/products/${productId}/suppliers/${supplierId}/unlink`);
  }

  // DELETE: api/products/{id}
  // Deletes a specific product.
  public deleteProduct(product: Product | string): Observable<Product> {
    const id = typeof product === 'string' ? product : (product as Product).id;
    return this.http.delete<Product>(`${environment.api}/products/${id}`);
  }
}

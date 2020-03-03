import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { ProductDetail } from 'src/app/shared/models/ProductDetail';

@Injectable({ providedIn: 'root' })
export class ProductDetailService {
  constructor(private http: HttpClient) {}

  // GET: api/productdetails
  // Retrieves all productdetails.
  public getProductDetails(): Observable<ProductDetail[]> {
    return this.http.get<ProductDetail[]>(`${environment.api}/productdetails`);
  }

  // GET: api/productdetails/{id}
  // Retrieves a specific productdetail.
  public getProductDetail(productDetail: ProductDetail | string): Observable<ProductDetail> {
    const id = typeof productDetail === 'string' ? productDetail : (productDetail as ProductDetail).id;
    return this.http.get<ProductDetail>(`${environment.api}/productdetails/${id}`);
  }

  // POST: api/productdetails
  // Creates a new productdetail.
  public createProductDetail(productDetail: ProductDetail): Observable<ProductDetail> {
    return this.http.post<ProductDetail>(`${environment.api}/productdetails`, productDetail);
  }

  // PUT: api/productdetails/{id}
  // Updates a specific productdetail.
  public updateProductDetail(productDetail: ProductDetail): Observable<ProductDetail> {
    return this.http.put<ProductDetail>(`${environment.api}/productdetails/${productDetail.id}`, productDetail);
  }

  // DELETE: api/productdetails/{id}
  // Deletes a specific productdetail.
  public deleteProductDetail(productDetail: ProductDetail | string): Observable<ProductDetail> {
    const id = typeof productDetail === 'string' ? productDetail : (productDetail as ProductDetail).id;
    return this.http.delete<ProductDetail>(`${environment.api}/productdetails/${id}`);
  }
}

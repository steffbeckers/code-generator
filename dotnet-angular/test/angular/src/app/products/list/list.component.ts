import { Component, OnInit } from '@angular/core';

// Models
import { Product } from 'src/app/shared/models/Product';

// Services
import { ProductService } from 'src/app/shared/services/ProductService';

@Component({
  selector: 'app-products-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ProductsListComponent implements OnInit {
  public products: Product[];

  constructor(private productService: ProductService) {
    this.products = null;
  }

  ngOnInit(): void {
    this.getProducts();
  }

  private getProducts(): void {
    this.productService.getProducts().subscribe(
      (products: Product[]) => {
        this.products = products;
      }
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Product } from 'src/app/shared/models/Product';

// Services
import { ProductService } from 'src/app/shared/services/ProductService';

@Component({
  selector: 'app-product-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class ProductLinkComponent implements OnInit {
  // Product
  public product: Product;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService
  ) {
    this.product = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getProduct(routeParams.id);
    });
  }

  private getProduct(id: string): void {
    this.productService.getProduct(id).subscribe(
      (product: Product) => {
        this.product = product;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Product could not be found.');
          this.router.navigateByUrl('/products');
        }
      }
    );
  }
}

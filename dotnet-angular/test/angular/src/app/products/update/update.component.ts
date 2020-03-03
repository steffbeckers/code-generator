import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Product } from 'src/app/shared/models/Product';

// Services
import { ProductService } from 'src/app/shared/services/ProductService';

@Component({
  selector: 'app-product-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ProductUpdateComponent implements OnInit {
  // Product
  public product: Product;
  public productForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productService: ProductService
  ) {
    this.product = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      code: [''],
      quantity: [''],
      price: [''],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getProduct(routeParams.id);
    });
  }

  private getProduct(id: string): void {
    this.productService.getProduct(id).subscribe(
      (product: Product) => {
        this.product = product;
        this.productForm.patchValue(this.product);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Product could not be found.');
          this.router.navigateByUrl('/products');
        }
      }
    );
  }

  public updateProduct(andClose: boolean = false): void {
    // Validate
    if (this.productForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.productForm.pristine && andClose) {
      this.router.navigateByUrl('/products/' + this.product.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.productService.updateProduct(this.productForm.value).subscribe(
      (product: Product) => {
        if (andClose) {
          this.router.navigateByUrl('/products/' + product.id);
        }

        this.product = product;
        this.productForm.patchValue(this.product);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteProduct(): void {
    // Validate
    if (!this.product && !this.product.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete product: ' + this.product.id + '?')) {
      this.productService.deleteProduct(this.product.id).subscribe(
        () => {
          this.router.navigateByUrl('/products');
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
}

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Product } from 'src/app/shared/models/Product';

// Services
import { ProductService } from 'src/app/shared/services/ProductService';

@Component({
  selector: 'app-product-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ProductCreateComponent implements OnInit {
  // Product
  public productForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productService: ProductService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      code: [''],
      quantity: [''],
      price: [''],
      supplierId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.productForm.patchValue(queryParams);
    });
  }

  public createProduct(): void {
    // Validate
    if (this.productForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.productService.createProduct(this.productForm.value).subscribe(
      (product: Product) => {
        this.creating = false;

        this.router.navigateByUrl('/products/' + product.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

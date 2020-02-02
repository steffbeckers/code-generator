import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { ProductDetail } from 'src/app/shared/models/ProductDetail';

// Services
import { ProductDetailService } from 'src/app/shared/services/ProductDetailService';

@Component({
  selector: 'app-productdetail-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ProductDetailCreateComponent implements OnInit {
  // ProductDetail
  public productDetailForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productDetailService: ProductDetailService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.productDetailForm = this.fb.group({
      comment: ['', Validators.required],
      productId: ['', Validators.required],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.productDetailForm.patchValue(queryParams);
    });
  }

  public createProductDetail(): void {
    // Validate
    if (this.productDetailForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.productDetailService.createProductDetail(this.productDetailForm.value).subscribe(
      (productDetail: ProductDetail) => {
        this.creating = false;

        this.router.navigateByUrl('/productdetails/' + productDetail.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

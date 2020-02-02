import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { ProductDetail } from 'src/app/shared/models/ProductDetail';

// Services
import { ProductDetailService } from 'src/app/shared/services/ProductDetailService';

@Component({
  selector: 'app-productdetail-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ProductDetailUpdateComponent implements OnInit {
  // ProductDetail
  public productDetail: ProductDetail;
  public productDetailForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private productDetailService: ProductDetailService
  ) {
    this.productDetail = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.productDetailForm = this.fb.group({
      id: ['', Validators.required],
      comment: ['', Validators.required],
      productId: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getProductDetail(routeParams.id);
    });
  }

  private getProductDetail(id: string): void {
    this.productDetailService.getProductDetail(id).subscribe(
      (productDetail: ProductDetail) => {
        this.productDetail = productDetail;
        this.productDetailForm.patchValue(this.productDetail);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('ProductDetail could not be found.');
          this.router.navigateByUrl('/productdetails');
        }
      }
    );
  }

  public updateProductDetail(andClose: boolean = false): void {
    // Validate
    if (this.productDetailForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.productDetailForm.pristine && andClose) {
      this.router.navigateByUrl('/productdetails/' + this.productDetail.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.productDetailService.updateProductDetail(this.productDetailForm.value).subscribe(
      (productDetail: ProductDetail) => {
        if (andClose) {
          this.router.navigateByUrl('/productdetails/' + productDetail.id);
        }

        this.productDetail = productDetail;
        this.productDetailForm.patchValue(this.productDetail);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteProductDetail(): void {
    // Validate
    if (!this.productDetail && !this.productDetail.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete productdetail: ' + this.productDetail.id + '?')) {
      this.productDetailService.deleteProductDetail(this.productDetail.id).subscribe(
        () => {
          this.router.navigateByUrl('/productdetails');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('ProductDetail could not be found.');
            this.router.navigateByUrl('/productdetails');
          }
        }
      );
    }
  }
}

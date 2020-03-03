import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { ProductDetail } from 'src/app/shared/models/ProductDetail';

// Services
import { ProductDetailService } from 'src/app/shared/services/ProductDetailService';

@Component({
  selector: 'app-productdetail-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class ProductDetailLinkComponent implements OnInit {
  // ProductDetail
  public productDetail: ProductDetail;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productDetailService: ProductDetailService
  ) {
    this.productDetail = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getProductDetail(routeParams.id);
    });
  }

  private getProductDetail(id: string): void {
    this.productDetailService.getProductDetail(id).subscribe(
      (productDetail: ProductDetail) => {
        this.productDetail = productDetail;
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

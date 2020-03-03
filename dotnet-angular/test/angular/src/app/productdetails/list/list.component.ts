import { Component, OnInit } from '@angular/core';

// Models
import { ProductDetail } from 'src/app/shared/models/ProductDetail';

// Services
import { ProductDetailService } from 'src/app/shared/services/ProductDetailService';

@Component({
  selector: 'app-productdetails-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ProductDetailsListComponent implements OnInit {
  public productdetails: ProductDetail[];

  constructor(private productDetailService: ProductDetailService) {
    this.productdetails = null;
  }

  ngOnInit(): void {
    this.getProductDetails();
  }

  private getProductDetails(): void {
    this.productDetailService.getProductDetails().subscribe(
      (productdetails: ProductDetail[]) => {
        this.productdetails = productdetails;
      }
    );
  }
}

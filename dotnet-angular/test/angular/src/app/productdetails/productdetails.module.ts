import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailsRoutingModule } from './productdetails-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ProductDetailsListComponent } from '../productdetails/list/list.component';
import { ProductDetailDetailComponent } from '../productdetails/detail/detail.component';
import { ProductDetailCreateComponent } from '../productdetails/create/create.component';
import { ProductDetailUpdateComponent } from '../productdetails/update/update.component';
import { ProductDetailLinkComponent } from '../productdetails/link/link.component';

@NgModule({
  declarations: [
    ProductDetailsListComponent,
    ProductDetailDetailComponent,
    ProductDetailCreateComponent,
    ProductDetailUpdateComponent,
    ProductDetailLinkComponent,
  ],
  imports: [CommonModule, ProductDetailsRoutingModule, SharedModule]
})
export class ProductDetailsModule {}

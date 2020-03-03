import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsRoutingModule } from './products-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ProductsListComponent } from '../products/list/list.component';
import { ProductDetailComponent } from '../products/detail/detail.component';
import { ProductCreateComponent } from '../products/create/create.component';
import { ProductUpdateComponent } from '../products/update/update.component';
import { ProductLinkComponent } from '../products/link/link.component';

@NgModule({
  declarations: [
    ProductsListComponent,
    ProductDetailComponent,
    ProductCreateComponent,
    ProductUpdateComponent,
    ProductLinkComponent,
  ],
  imports: [CommonModule, ProductsRoutingModule, SharedModule]
})
export class ProductsModule {}

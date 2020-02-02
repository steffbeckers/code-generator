import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ProductDetailsListComponent } from './list/list.component';
import { ProductDetailDetailComponent } from './detail/detail.component';
import { ProductDetailCreateComponent } from './create/create.component';
import { ProductDetailUpdateComponent } from './update/update.component';
import { ProductDetailLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: ProductDetailCreateComponent
  },
  {
    path: ':id/link/:model',
    component: ProductDetailLinkComponent
  },
  {
    path: ':id/edit',
    component: ProductDetailUpdateComponent
  },
  {
    path: ':id',
    component: ProductDetailDetailComponent
  },
  {
    path: '',
    component: ProductDetailsListComponent
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductDetailsRoutingModule {}

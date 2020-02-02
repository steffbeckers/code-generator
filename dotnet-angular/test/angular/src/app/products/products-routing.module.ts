import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ProductsListComponent } from './list/list.component';
import { ProductDetailComponent } from './detail/detail.component';
import { ProductCreateComponent } from './create/create.component';
import { ProductUpdateComponent } from './update/update.component';
import { ProductLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: ProductCreateComponent
  },
  {
    path: ':id/link/:model',
    component: ProductLinkComponent
  },
  {
    path: ':id/edit',
    component: ProductUpdateComponent
  },
  {
    path: ':id',
    component: ProductDetailComponent
  },
  {
    path: '',
    component: ProductsListComponent
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
export class ProductsRoutingModule {}

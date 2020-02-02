import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { SuppliersListComponent } from './list/list.component';
import { SupplierDetailComponent } from './detail/detail.component';
import { SupplierCreateComponent } from './create/create.component';
import { SupplierUpdateComponent } from './update/update.component';
import { SupplierLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: SupplierCreateComponent
  },
  {
    path: ':id/link/:model',
    component: SupplierLinkComponent
  },
  {
    path: ':id/edit',
    component: SupplierUpdateComponent
  },
  {
    path: ':id',
    component: SupplierDetailComponent
  },
  {
    path: '',
    component: SuppliersListComponent
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
export class SuppliersRoutingModule {}

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { AddressesListComponent } from './list/list.component';
import { AddressDetailComponent } from './detail/detail.component';
import { AddressCreateComponent } from './create/create.component';
import { AddressUpdateComponent } from './update/update.component';
import { AddressLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: AddressCreateComponent
  },
  {
    path: ':id/link',
    component: AddressLinkComponent
  },
  {
    path: ':id/edit',
    component: AddressUpdateComponent
  },
  {
    path: ':id',
    component: AddressDetailComponent
  },
  {
    path: '',
    component: AddressesListComponent
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
export class AddressesRoutingModule {}

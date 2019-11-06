import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { AddressesListComponent } from './list/list.component';
import { AddressCreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: AddressCreateComponent
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

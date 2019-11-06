import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { AccountsListComponent } from './list/list.component';
import { AccountDetailComponent } from './detail/detail.component';
import { AccountCreateComponent } from './create/create.component';
import { AccountUpdateComponent } from './update/update.component';

const routes: Routes = [
  {
    path: 'create',
    component: AccountCreateComponent
  },
  {
    path: ':id/edit',
    component: AccountUpdateComponent
  },
  {
    path: ':id',
    component: AccountDetailComponent
  },
  {
    path: '',
    component: AccountsListComponent
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
export class AccountsRoutingModule {}

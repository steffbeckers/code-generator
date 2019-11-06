import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { AccountsListComponent } from './list/list.component';
import { AccountCreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: AccountCreateComponent
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

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { extract } from '@app/core';
import { Shell } from '@app/shell/shell.service';
import { AccountListComponent } from './account-list/account-list.component';

const routes: Routes = [
  Shell.childRoutes([{ path: 'accounts', component: AccountListComponent, data: { title: extract('Accounts') } }])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountsRoutingModule {}

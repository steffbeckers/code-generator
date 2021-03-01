import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountsComponent } from './accounts.component';
import { AccountsDetailComponent } from './detail/detail.component';
import { AccountsEditComponent } from './edit/edit.component';
import { AccountsListComponent } from './list/list.component';

const routes: Routes = [
  {
    path: '',
    component: AccountsComponent,
    children: [
      { path: ':id/edit', component: AccountsEditComponent },
      { path: ':id', component: AccountsDetailComponent },
      { path: '', component: AccountsListComponent },
      { path: '**', pathMatch: 'full', redirectTo: '' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AccountsRoutingModule {}

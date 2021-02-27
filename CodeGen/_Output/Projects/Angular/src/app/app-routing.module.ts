import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutDefaultComponent } from './layouts/default/default.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutDefaultComponent,
    children: [
      {
        path: 'accounts',
        loadChildren: () =>
          import('./modules/accounts/accounts.module').then(
            (m) => m.AccountsModule
          ),
      },
      {
        path: 'contacts',
        loadChildren: () =>
          import('./modules/contacts/contacts.module').then(
            (m) => m.ContactsModule
          ),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

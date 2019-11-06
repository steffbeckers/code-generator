import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'accounts',
    loadChildren: './accounts/accounts.module#AccountsModule',
  },
  {
    path: 'contacts',
    loadChildren: './contacts/contacts.module#ContactsModule',
  },
  {
    path: 'addresses',
    loadChildren: './addresses/addresses.module#AddressesModule',
  },
  {
    path: 'notes',
    loadChildren: './notes/notes.module#NotesModule',
  },
  {
    path: 'todos',
    loadChildren: './todos/todos.module#TodosModule',
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

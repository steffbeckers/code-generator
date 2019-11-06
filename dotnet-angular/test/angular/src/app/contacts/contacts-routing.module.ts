import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ContactsListComponent } from './list/list.component';
import { ContactCreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: ContactCreateComponent
  },
  {
    path: '',
    component: ContactsListComponent
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
export class ContactsRoutingModule {}

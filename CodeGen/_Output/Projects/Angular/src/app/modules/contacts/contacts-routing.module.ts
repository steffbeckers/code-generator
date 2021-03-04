import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactsComponent } from './contacts.component';
import { ContactsCreateComponent } from './create/create.component';
import { ContactsDetailComponent } from './detail/detail.component';
import { ContactsEditComponent } from './edit/edit.component';
import { ContactsListComponent } from './list/list.component';

const routes: Routes = [
  {
    path: '',
    component: ContactsComponent,
    children: [
      { path: 'new', component: ContactsCreateComponent },
      { path: ':id/edit', component: ContactsEditComponent },
      { path: ':id', component: ContactsDetailComponent },
      { path: '', component: ContactsListComponent },
      { path: '**', pathMatch: 'full', redirectTo: '' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ContactsRoutingModule {}

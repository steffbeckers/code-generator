import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ContactsListComponent } from './list/list.component';
import { ContactDetailComponent } from './detail/detail.component';
import { ContactCreateComponent } from './create/create.component';
import { ContactUpdateComponent } from './update/update.component';
import { ContactLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: ContactCreateComponent
  },
  {
    path: ':id/link',
    component: ContactLinkComponent
  },
  {
    path: ':id/edit',
    component: ContactUpdateComponent
  },
  {
    path: ':id',
    component: ContactDetailComponent
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

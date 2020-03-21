import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { DocumentsListComponent } from './list/list.component';
import { DocumentDetailComponent } from './detail/detail.component';
import { DocumentCreateComponent } from './create/create.component';
import { DocumentUpdateComponent } from './update/update.component';
import { DocumentLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: DocumentCreateComponent
  },
  {
    path: ':id/link/:model',
    component: DocumentLinkComponent
  },
  {
    path: ':id/edit',
    component: DocumentUpdateComponent
  },
  {
    path: ':id',
    component: DocumentDetailComponent
  },
  {
    path: '',
    component: DocumentsListComponent
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
export class DocumentsRoutingModule {}

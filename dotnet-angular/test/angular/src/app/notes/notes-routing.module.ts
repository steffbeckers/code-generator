import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { NotesListComponent } from './list/list.component';
import { NoteDetailComponent } from './detail/detail.component';
import { NoteCreateComponent } from './create/create.component';
import { NoteUpdateComponent } from './update/update.component';
import { NoteLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: NoteCreateComponent
  },
  {
    path: ':id/link/:model',
    component: NoteLinkComponent
  },
  {
    path: ':id/edit',
    component: NoteUpdateComponent
  },
  {
    path: ':id',
    component: NoteDetailComponent
  },
  {
    path: '',
    component: NotesListComponent
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
export class NotesRoutingModule {}

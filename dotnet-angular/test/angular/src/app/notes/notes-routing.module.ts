import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { NotesListComponent } from './list/list.component';
import { NoteDetailComponent } from './detail/detail.component';
import { NoteCreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: NoteCreateComponent
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

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { NotesListComponent } from './list/list.component';
import { NoteCreateComponent } from './create/create.component';

const routes: Routes = [
  {
    path: 'create',
    component: NoteCreateComponent
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

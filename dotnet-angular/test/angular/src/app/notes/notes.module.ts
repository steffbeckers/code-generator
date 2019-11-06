import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotesRoutingModule } from './notes-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { NotesListComponent } from '../notes/list/list.component';
import { NoteDetailComponent } from '../notes/detail/detail.component';
import { NoteCreateComponent } from '../notes/create/create.component';
import { NoteUpdateComponent } from '../notes/update/update.component';

@NgModule({
  declarations: [
    NotesListComponent,
    NoteDetailComponent,
    NoteCreateComponent,
    NoteUpdateComponent,
  ],
  imports: [CommonModule, NotesRoutingModule, SharedModule]
})
export class NotesModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotesRoutingModule } from './notes-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { NotesListComponent } from '../notes/list/list.component';
import { NoteCreateComponent } from '../notes/create/create.component';

@NgModule({
  declarations: [
    NotesListComponent,
    NoteCreateComponent,
  ],
  imports: [CommonModule, NotesRoutingModule, SharedModule]
})
export class NotesModule {}

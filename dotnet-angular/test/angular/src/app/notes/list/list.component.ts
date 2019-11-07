import { Component, OnInit } from '@angular/core';

// Models
import { Note } from 'src/app/shared/models/Note';

// Services
import { NoteService } from 'src/app/shared/services/NoteService';

@Component({
  selector: 'app-notes-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class NotesListComponent implements OnInit {
  public notes: Note[];

  constructor(private noteService: NoteService) {
    this.notes = null;
  }

  ngOnInit(): void {
    this.getNotes();
  }

  private getNotes(): void {
    this.noteService.getNotes().subscribe(
      (notes: Note[]) => {
        this.notes = notes;
      }
    );
  }
}

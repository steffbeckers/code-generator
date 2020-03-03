import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Note } from 'src/app/shared/models/Note';

// Services
import { NoteService } from 'src/app/shared/services/NoteService';

@Component({
  selector: 'app-note-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class NoteDetailComponent implements OnInit {
  // Note
  public note: Note;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private noteService: NoteService
  ) {
    this.note = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getNote(routeParams.id);
    });
  }

  private getNote(id: string): void {
    this.noteService.getNote(id).subscribe(
      (note: Note) => {
        this.note = note;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Note could not be found.');
          this.router.navigateByUrl('/notes');
        }
      }
    );
  }

  public deleteNote(): void {
    // Validate
    if (!this.note && !this.note.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete note: ' + this.note.title + '?')) {
      this.noteService.deleteNote(this.note.id).subscribe(
        () => {
          this.router.navigateByUrl('/notes');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Note could not be found.');
            this.router.navigateByUrl('/notes');
          }
        }
      );
    }
  }
}

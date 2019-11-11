import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Note } from 'src/app/shared/models/Note';

// Services
import { NoteService } from 'src/app/shared/services/NoteService';

@Component({
  selector: 'app-note-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class NoteLinkComponent implements OnInit {
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
}

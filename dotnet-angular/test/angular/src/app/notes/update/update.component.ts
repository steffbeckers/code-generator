import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Note } from 'src/app/shared/models/Note';

// Services
import { NoteService } from 'src/app/shared/services/NoteService';

@Component({
  selector: 'app-note-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class NoteUpdateComponent implements OnInit {
  // Note
  public note: Note;
  public noteForm: FormGroup;
  public updating = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private noteService: NoteService
  ) {}

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getNote(routeParams.id);
    });

    this.noteForm = this.fb.group({
      id: ['', Validators.required],
      title: ['', Validators.required],
      body: [''],
    });
  }

  private getNote(id: string): void {
    this.noteService.getNote(id).subscribe(
      (note: Note) => {
        this.note = note;
        this.noteForm.patchValue(this.note);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Note could not be found.');
          this.router.navigateByUrl('/notes');
        }
      }
    );
  }

  public updateNote(andClose: boolean = false): void {
    // Validate
    if (this.noteForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.noteForm.pristine && andClose) {
      this.router.navigateByUrl('/notes/' + this.note.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.noteService.updateNote(this.noteForm.value).subscribe(
      (note: Note) => {
        if (andClose) {
          this.router.navigateByUrl('/notes/' + note.id);
        }

        this.note = note;
        this.noteForm.patchValue(this.note);
      },
      null,
      () => {
        this.updating = false;
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

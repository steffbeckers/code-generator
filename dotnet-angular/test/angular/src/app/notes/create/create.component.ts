import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Note } from 'src/app/shared/models/Note';

// Services
import { NoteService } from 'src/app/shared/services/NoteService';

@Component({
  selector: 'app-note-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class NoteCreateComponent implements OnInit {
  // Note
  public noteForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private noteService: NoteService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.noteForm = this.fb.group({
      title: ['', Validators.required],
      body: [''],
    });
  }

  public createNote(): void {
    // Validate
    if (this.noteForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.noteService.createNote(this.noteForm.value).subscribe(
      (note: Note) => {
        this.creating = false;

        this.router.navigateByUrl('/notes/' + note.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

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
  public creating = false;

  constructor(
    private router: Router,
    public fb: FormBuilder,
    public noteService: NoteService
  ) {}

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
    this.creating = true;

    this.noteService.createNote(this.noteForm.value).subscribe(
      () => {
        this.router.navigateByUrl('/notes');
      }
    );
  }
}

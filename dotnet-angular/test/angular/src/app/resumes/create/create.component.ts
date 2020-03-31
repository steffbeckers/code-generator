import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Resume } from 'src/app/shared/models/Resume';

// Services
import { ResumeService } from 'src/app/shared/services/ResumeService';

@Component({
  selector: 'app-resume-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ResumeCreateComponent implements OnInit {
  // Resume
  public resumeForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private resumeService: ResumeService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.resumeForm = this.fb.group({
      jobTitle: [''],
      description: [''],
      resumeStateId: ['', Validators.required],
      documentId: [''],
      skillId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.resumeForm.patchValue(queryParams);
    });
  }

  public createResume(): void {
    // Validate
    if (this.resumeForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.resumeService.createResume(this.resumeForm.value).subscribe(
      (resume: Resume) => {
        this.creating = false;

        this.router.navigateByUrl('/resumes/' + resume.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

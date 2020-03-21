import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Resume } from 'src/app/shared/models/Resume';

// Services
import { ResumeService } from 'src/app/shared/services/ResumeService';

@Component({
  selector: 'app-resume-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ResumeUpdateComponent implements OnInit {
  // Resume
  public resume: Resume;
  public resumeForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private resumeService: ResumeService
  ) {
    this.resume = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.resumeForm = this.fb.group({
      id: ['', Validators.required],
      jobTitle: [''],
      description: [''],
      resumeStateId: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getResume(routeParams.id);
    });
  }

  private getResume(id: string): void {
    this.resumeService.getResume(id).subscribe(
      (resume: Resume) => {
        this.resume = resume;
        this.resumeForm.patchValue(this.resume);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Resume could not be found.');
          this.router.navigateByUrl('/resumes');
        }
      }
    );
  }

  public updateResume(andClose: boolean = false): void {
    // Validate
    if (this.resumeForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.resumeForm.pristine && andClose) {
      this.router.navigateByUrl('/resumes/' + this.resume.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.resumeService.updateResume(this.resumeForm.value).subscribe(
      (resume: Resume) => {
        if (andClose) {
          this.router.navigateByUrl('/resumes/' + resume.id);
        }

        this.resume = resume;
        this.resumeForm.patchValue(this.resume);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteResume(): void {
    // Validate
    if (!this.resume && !this.resume.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete resume: ' + this.resume.id + '?')) {
      this.resumeService.deleteResume(this.resume.id).subscribe(
        () => {
          this.router.navigateByUrl('/resumes');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Resume could not be found.');
            this.router.navigateByUrl('/resumes');
          }
        }
      );
    }
  }
}

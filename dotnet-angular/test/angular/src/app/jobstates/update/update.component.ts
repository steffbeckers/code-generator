import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { JobState } from 'src/app/shared/models/JobState';

// Services
import { JobStateService } from 'src/app/shared/services/JobStateService';

@Component({
  selector: 'app-jobstate-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class JobStateUpdateComponent implements OnInit {
  // JobState
  public jobState: JobState;
  public jobStateForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private jobStateService: JobStateService
  ) {
    this.jobState = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.jobStateForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      displayName: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getJobState(routeParams.id);
    });
  }

  private getJobState(id: string): void {
    this.jobStateService.getJobState(id).subscribe(
      (jobState: JobState) => {
        this.jobState = jobState;
        this.jobStateForm.patchValue(this.jobState);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('JobState could not be found.');
          this.router.navigateByUrl('/jobstates');
        }
      }
    );
  }

  public updateJobState(andClose: boolean = false): void {
    // Validate
    if (this.jobStateForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.jobStateForm.pristine && andClose) {
      this.router.navigateByUrl('/jobstates/' + this.jobState.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.jobStateService.updateJobState(this.jobStateForm.value).subscribe(
      (jobState: JobState) => {
        if (andClose) {
          this.router.navigateByUrl('/jobstates/' + jobState.id);
        }

        this.jobState = jobState;
        this.jobStateForm.patchValue(this.jobState);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteJobState(): void {
    // Validate
    if (!this.jobState && !this.jobState.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete jobstate: ' + this.jobState.id + '?')) {
      this.jobStateService.deleteJobState(this.jobState.id).subscribe(
        () => {
          this.router.navigateByUrl('/jobstates');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('JobState could not be found.');
            this.router.navigateByUrl('/jobstates');
          }
        }
      );
    }
  }
}

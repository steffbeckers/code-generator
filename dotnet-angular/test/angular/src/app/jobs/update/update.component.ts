import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Job } from 'src/app/shared/models/Job';

// Services
import { JobService } from 'src/app/shared/services/JobService';

@Component({
  selector: 'app-job-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class JobUpdateComponent implements OnInit {
  // Job
  public job: Job;
  public jobForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private jobService: JobService
  ) {
    this.job = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.jobForm = this.fb.group({
      id: ['', Validators.required],
      title: [''],
      description: [''],
      jobStateId: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getJob(routeParams.id);
    });
  }

  private getJob(id: string): void {
    this.jobService.getJob(id).subscribe(
      (job: Job) => {
        this.job = job;
        this.jobForm.patchValue(this.job);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Job could not be found.');
          this.router.navigateByUrl('/jobs');
        }
      }
    );
  }

  public updateJob(andClose: boolean = false): void {
    // Validate
    if (this.jobForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.jobForm.pristine && andClose) {
      this.router.navigateByUrl('/jobs/' + this.job.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.jobService.updateJob(this.jobForm.value).subscribe(
      (job: Job) => {
        if (andClose) {
          this.router.navigateByUrl('/jobs/' + job.id);
        }

        this.job = job;
        this.jobForm.patchValue(this.job);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteJob(): void {
    // Validate
    if (!this.job && !this.job.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete job: ' + this.job.id + '?')) {
      this.jobService.deleteJob(this.job.id).subscribe(
        () => {
          this.router.navigateByUrl('/jobs');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Job could not be found.');
            this.router.navigateByUrl('/jobs');
          }
        }
      );
    }
  }
}

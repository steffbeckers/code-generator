import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Job } from 'src/app/shared/models/Job';

// Services
import { JobService } from 'src/app/shared/services/JobService';

@Component({
  selector: 'app-job-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class JobCreateComponent implements OnInit {
  // Job
  public jobForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private jobService: JobService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.jobForm = this.fb.group({
      title: [''],
      description: [''],
      jobStateId: ['', Validators.required],
      skillId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.jobForm.patchValue(queryParams);
    });
  }

  public createJob(): void {
    // Validate
    if (this.jobForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.jobService.createJob(this.jobForm.value).subscribe(
      (job: Job) => {
        this.creating = false;

        this.router.navigateByUrl('/jobs/' + job.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

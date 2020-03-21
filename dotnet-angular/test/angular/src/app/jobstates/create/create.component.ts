import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { JobState } from 'src/app/shared/models/JobState';

// Services
import { JobStateService } from 'src/app/shared/services/JobStateService';

@Component({
  selector: 'app-jobstate-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class JobStateCreateComponent implements OnInit {
  // JobState
  public jobStateForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private jobStateService: JobStateService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.jobStateForm = this.fb.group({
      name: ['', Validators.required],
      displayName: ['', Validators.required],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.jobStateForm.patchValue(queryParams);
    });
  }

  public createJobState(): void {
    // Validate
    if (this.jobStateForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.jobStateService.createJobState(this.jobStateForm.value).subscribe(
      (jobState: JobState) => {
        this.creating = false;

        this.router.navigateByUrl('/jobstates/' + jobState.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

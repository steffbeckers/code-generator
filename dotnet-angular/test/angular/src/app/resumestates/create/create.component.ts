import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

// Services
import { ResumeStateService } from 'src/app/shared/services/ResumeStateService';

@Component({
  selector: 'app-resumestate-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ResumeStateCreateComponent implements OnInit {
  // ResumeState
  public resumeStateForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private resumeStateService: ResumeStateService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.resumeStateForm = this.fb.group({
      name: ['', Validators.required],
      displayName: ['', Validators.required],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.resumeStateForm.patchValue(queryParams);
    });
  }

  public createResumeState(): void {
    // Validate
    if (this.resumeStateForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.resumeStateService.createResumeState(this.resumeStateForm.value).subscribe(
      (resumeState: ResumeState) => {
        this.creating = false;

        this.router.navigateByUrl('/resumestates/' + resumeState.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

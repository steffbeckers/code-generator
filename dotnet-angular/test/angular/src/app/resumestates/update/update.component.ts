import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

// Services
import { ResumeStateService } from 'src/app/shared/services/ResumeStateService';

@Component({
  selector: 'app-resumestate-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ResumeStateUpdateComponent implements OnInit {
  // ResumeState
  public resumeState: ResumeState;
  public resumeStateForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private resumeStateService: ResumeStateService
  ) {
    this.resumeState = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.resumeStateForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      displayName: ['', Validators.required],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getResumeState(routeParams.id);
    });
  }

  private getResumeState(id: string): void {
    this.resumeStateService.getResumeState(id).subscribe(
      (resumeState: ResumeState) => {
        this.resumeState = resumeState;
        this.resumeStateForm.patchValue(this.resumeState);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('ResumeState could not be found.');
          this.router.navigateByUrl('/resumestates');
        }
      }
    );
  }

  public updateResumeState(andClose: boolean = false): void {
    // Validate
    if (this.resumeStateForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.resumeStateForm.pristine && andClose) {
      this.router.navigateByUrl('/resumestates/' + this.resumeState.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.resumeStateService.updateResumeState(this.resumeStateForm.value).subscribe(
      (resumeState: ResumeState) => {
        if (andClose) {
          this.router.navigateByUrl('/resumestates/' + resumeState.id);
        }

        this.resumeState = resumeState;
        this.resumeStateForm.patchValue(this.resumeState);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteResumeState(): void {
    // Validate
    if (!this.resumeState && !this.resumeState.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete resumestate: ' + this.resumeState.id + '?')) {
      this.resumeStateService.deleteResumeState(this.resumeState.id).subscribe(
        () => {
          this.router.navigateByUrl('/resumestates');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('ResumeState could not be found.');
            this.router.navigateByUrl('/resumestates');
          }
        }
      );
    }
  }
}

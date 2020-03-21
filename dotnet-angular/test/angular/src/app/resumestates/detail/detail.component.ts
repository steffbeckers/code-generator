import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

// Services
import { ResumeStateService } from 'src/app/shared/services/ResumeStateService';

@Component({
  selector: 'app-resumestate-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class ResumeStateDetailComponent implements OnInit {
  // ResumeState
  public resumeState: ResumeState;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private resumeStateService: ResumeStateService
  ) {
    this.resumeState = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getResumeState(routeParams.id);
    });
  }

  private getResumeState(id: string): void {
    this.resumeStateService.getResumeState(id).subscribe(
      (resumeState: ResumeState) => {
        this.resumeState = resumeState;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('ResumeState could not be found.');
          this.router.navigateByUrl('/resumestates');
        }
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

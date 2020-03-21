import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

// Services
import { ResumeStateService } from 'src/app/shared/services/ResumeStateService';

@Component({
  selector: 'app-resumestate-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class ResumeStateLinkComponent implements OnInit {
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
}

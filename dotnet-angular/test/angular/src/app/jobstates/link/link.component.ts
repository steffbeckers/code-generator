import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { JobState } from 'src/app/shared/models/JobState';

// Services
import { JobStateService } from 'src/app/shared/services/JobStateService';

@Component({
  selector: 'app-jobstate-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class JobStateLinkComponent implements OnInit {
  // JobState
  public jobState: JobState;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private jobStateService: JobStateService
  ) {
    this.jobState = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getJobState(routeParams.id);
    });
  }

  private getJobState(id: string): void {
    this.jobStateService.getJobState(id).subscribe(
      (jobState: JobState) => {
        this.jobState = jobState;
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

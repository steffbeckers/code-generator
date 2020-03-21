import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Job } from 'src/app/shared/models/Job';

// Services
import { JobService } from 'src/app/shared/services/JobService';

@Component({
  selector: 'app-job-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class JobLinkComponent implements OnInit {
  // Job
  public job: Job;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private jobService: JobService
  ) {
    this.job = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getJob(routeParams.id);
    });
  }

  private getJob(id: string): void {
    this.jobService.getJob(id).subscribe(
      (job: Job) => {
        this.job = job;
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

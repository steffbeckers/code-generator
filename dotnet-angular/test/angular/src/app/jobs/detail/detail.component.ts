import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Job } from 'src/app/shared/models/Job';

// Services
import { JobService } from 'src/app/shared/services/JobService';

@Component({
  selector: 'app-job-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class JobDetailComponent implements OnInit {
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

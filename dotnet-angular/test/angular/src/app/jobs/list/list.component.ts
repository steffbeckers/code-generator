import { Component, OnInit } from '@angular/core';

// Models
import { Job } from 'src/app/shared/models/Job';

// Services
import { JobService } from 'src/app/shared/services/JobService';

@Component({
  selector: 'app-jobs-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class JobsListComponent implements OnInit {
  public jobs: Job[];

  constructor(private jobService: JobService) {
    this.jobs = null;
  }

  ngOnInit(): void {
    this.getJobs();
  }

  private getJobs(): void {
    this.jobService.getJobs().subscribe(
      (jobs: Job[]) => {
        this.jobs = jobs;
      }
    );
  }
}

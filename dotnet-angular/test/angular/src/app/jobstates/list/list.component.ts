import { Component, OnInit } from '@angular/core';

// Models
import { JobState } from 'src/app/shared/models/JobState';

// Services
import { JobStateService } from 'src/app/shared/services/JobStateService';

@Component({
  selector: 'app-jobstates-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class JobStatesListComponent implements OnInit {
  public jobstates: JobState[];

  constructor(private jobStateService: JobStateService) {
    this.jobstates = null;
  }

  ngOnInit(): void {
    this.getJobStates();
  }

  private getJobStates(): void {
    this.jobStateService.getJobStates().subscribe(
      (jobstates: JobState[]) => {
        this.jobstates = jobstates;
      }
    );
  }
}

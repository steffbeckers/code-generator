import { Component, OnInit } from '@angular/core';

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

// Services
import { ResumeStateService } from 'src/app/shared/services/ResumeStateService';

@Component({
  selector: 'app-resumestates-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ResumeStatesListComponent implements OnInit {
  public resumestates: ResumeState[];

  constructor(private resumeStateService: ResumeStateService) {
    this.resumestates = null;
  }

  ngOnInit(): void {
    this.getResumeStates();
  }

  private getResumeStates(): void {
    this.resumeStateService.getResumeStates().subscribe(
      (resumestates: ResumeState[]) => {
        this.resumestates = resumestates;
      }
    );
  }
}

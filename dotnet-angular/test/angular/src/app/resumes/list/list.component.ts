import { Component, OnInit } from '@angular/core';

// Models
import { Resume } from 'src/app/shared/models/Resume';

// Services
import { ResumeService } from 'src/app/shared/services/ResumeService';

@Component({
  selector: 'app-resumes-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ResumesListComponent implements OnInit {
  public resumes: Resume[];

  constructor(private resumeService: ResumeService) {
    this.resumes = null;
  }

  ngOnInit(): void {
    this.getResumes();
  }

  private getResumes(): void {
    this.resumeService.getResumes().subscribe(
      (resumes: Resume[]) => {
        this.resumes = resumes;
      }
    );
  }
}

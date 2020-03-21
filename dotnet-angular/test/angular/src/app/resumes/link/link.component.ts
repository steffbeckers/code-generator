import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Resume } from 'src/app/shared/models/Resume';

// Services
import { ResumeService } from 'src/app/shared/services/ResumeService';

@Component({
  selector: 'app-resume-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class ResumeLinkComponent implements OnInit {
  // Resume
  public resume: Resume;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private resumeService: ResumeService
  ) {
    this.resume = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getResume(routeParams.id);
    });
  }

  private getResume(id: string): void {
    this.resumeService.getResume(id).subscribe(
      (resume: Resume) => {
        this.resume = resume;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Resume could not be found.');
          this.router.navigateByUrl('/resumes');
        }
      }
    );
  }
}

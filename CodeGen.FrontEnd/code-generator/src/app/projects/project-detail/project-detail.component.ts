import { Component, OnDestroy, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Project } from 'src/app/shared/models/project.model';
import { Response } from 'src/app/shared/models/response.model';
import { ProjectsService } from 'src/app/shared/services/projects.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-project-detail',
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.scss'],
})
export class ProjectDetailComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  project$: BehaviorSubject<Project> = new BehaviorSubject<Project>(null);
  outputUrl: SafeResourceUrl;

  constructor(
    private projectsService: ProjectsService,
    private router: Router,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.outputUrl = this.sanitizer.bypassSecurityTrustResourceUrl(
      environment.output
    );

    this.subs.push(
      this.route.paramMap.subscribe((params) => {
        const projectId = params.get('id');
        this.subs.push(
          this.projectsService
            .getProjectById(projectId)
            .subscribe((response: Response) => {
              if (!response.success) {
                this.router.navigateByUrl('/projects');
                return;
              }

              this.project$.next(response.data as Project);
            })
        );
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  generate(): void {
    this.subs.push(
      this.projectsService
        .generateProjectById(this.project$.value.id)
        .subscribe((response: Response) => {
          if (!response.success) {
            return;
          }

          // TODO
        })
    );
  }
}

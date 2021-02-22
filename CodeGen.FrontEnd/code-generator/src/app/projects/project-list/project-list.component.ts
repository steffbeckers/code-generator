import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Project } from 'src/app/shared/models/project.model';
import { Response } from 'src/app/shared/models/response.model';
import { ProjectsService } from 'src/app/shared/services/projects.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  projects$: BehaviorSubject<Project[]> = new BehaviorSubject<Project[]>([]);

  constructor(private projectsService: ProjectsService) {}

  ngOnInit(): void {
    this.projectsService.getProjects().subscribe((response: Response) => {
      if (!response.success) return;
      this.projects$.next(response.data as Project[]);
    });
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }
}

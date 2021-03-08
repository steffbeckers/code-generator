import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import * as ProjectsActions from 'src/app/projects/store/actions/projects.actions';
import {
  selectProjectsState,
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
  selectedProject,
} from 'src/app/projects/store/selectors/projects.selectors';

@Injectable()
export class ProjectsFacade {
  projectsState$ = this.store.select(selectProjectsState);
  ids$ = this.store.select(selectIds);
  projectsById$ = this.store.select(selectEntities);
  projects$ = this.store.select(selectAll);
  total$ = this.store.select(selectTotal);
  selectedProject$ = this.store.select(selectedProject);

  constructor(private store: Store) {}

  getProjects(): void {
    this.store.dispatch(ProjectsActions.getProjects());
  }

  getProjectById(id: string): void {
    this.store.dispatch(ProjectsActions.getProjectById({ id }));
  }

  selectProjectById(id: string) {
    this.store.dispatch(ProjectsActions.selectProjectById({ id }));
  }
}

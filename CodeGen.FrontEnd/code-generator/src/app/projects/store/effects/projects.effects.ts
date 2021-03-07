import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as ProjectsActions from '../actions/projects.actions';
import { ProjectsService } from 'src/app/shared/services/projects.service';
import { Response } from 'src/app/shared/models/response.model';

@Injectable()
export class ProjectsEffects {
  constructor(
    private actions$: Actions,
    private projectsService: ProjectsService
  ) {}

  loadProjects$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectsActions.loadProjects),
      switchMap(() =>
        this.projectsService.getProjects().pipe(
          map((response: Response) =>
            ProjectsActions.loadProjectsSuccess({ response })
          ),
          catchError((error) =>
            of(ProjectsActions.loadProjectsFailure({ error }))
          )
        )
      )
    );
  });

  loadProjectById$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectsActions.loadProjectsById),
      switchMap(({ id }) =>
        this.projectsService.getProjectById(id).pipe(
          map((response: Response) =>
            ProjectsActions.loadProjectsByIdSuccess({ response })
          ),
          catchError((error) =>
            of(ProjectsActions.loadProjectsFailure({ error }))
          )
        )
      )
    );
  });
}

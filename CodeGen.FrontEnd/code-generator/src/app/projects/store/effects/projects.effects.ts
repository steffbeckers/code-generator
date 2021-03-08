import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap } from 'rxjs/operators';
import { of } from 'rxjs';

import * as ProjectsActions from '../actions/projects.actions';
import { Response } from 'src/app/shared/models/response.model';
import { ProjectsService } from '../../projects.service';

@Injectable()
export class ProjectsEffects {
  constructor(
    private actions$: Actions,
    private projectsService: ProjectsService
  ) {}

  loadProjects$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectsActions.getProjects),
      switchMap(() =>
        this.projectsService.getProjects().pipe(
          map((response: Response) =>
            ProjectsActions.getProjectsSuccess({ response })
          ),
          catchError((error) =>
            of(ProjectsActions.getProjectsFailure({ error }))
          )
        )
      )
    );
  });

  loadProjectById$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProjectsActions.getProjectById),
      switchMap(({ id }) =>
        this.projectsService.getProjectById(id).pipe(
          map((response: Response) =>
            ProjectsActions.getProjectByIdSuccess({ response })
          ),
          catchError((error) =>
            of(ProjectsActions.getProjectsFailure({ error }))
          )
        )
      )
    );
  });
}

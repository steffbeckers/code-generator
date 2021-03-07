import { createAction, props } from '@ngrx/store';
import { Project } from 'src/app/shared/models/project.model';
import { Response } from 'src/app/shared/models/response.model';

export const loadProjects = createAction('[Projects] Load Projects');
export const loadProjectsSuccess = createAction(
  '[Projects] Load Projects Success',
  props<{ response: Response }>()
);
export const loadProjectsFailure = createAction(
  '[Projects] Load Projects Failure',
  props<{ error: any }>()
);

export const loadProjectsById = createAction(
  '[Projects] Load Project by id',
  props<{ id: string }>()
);
export const loadProjectsByIdSuccess = createAction(
  '[Projects] Load Project by id Success',
  props<{ response: Response }>()
);
export const loadProjectsByIdFailure = createAction(
  '[Projects] Load Project by id Failure',
  props<{ error: any }>()
);

export const selectProject = createAction(
  '[Projects] Select project',
  props<{ project: Project }>()
);

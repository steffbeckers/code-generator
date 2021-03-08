import { createAction, props } from '@ngrx/store';
import { Response } from 'src/app/shared/models/response.model';

export const getProjects = createAction('[Projects] Get projects');
export const getProjectsSuccess = createAction(
  '[Projects] Get projects Success',
  props<{ response: Response }>()
);
export const getProjectsFailure = createAction(
  '[Projects] Get projects Failure',
  props<{ error: any }>()
);

export const getProjectById = createAction(
  '[Projects] Get project by id',
  props<{ id: string }>()
);
export const getProjectByIdSuccess = createAction(
  '[Projects] Get project by id Success',
  props<{ response: Response }>()
);
export const getProjectByIdFailure = createAction(
  '[Projects] Get project by id Failure',
  props<{ error: any }>()
);

export const selectProjectById = createAction(
  '[Projects] Select project by id',
  props<{ id: string }>()
);

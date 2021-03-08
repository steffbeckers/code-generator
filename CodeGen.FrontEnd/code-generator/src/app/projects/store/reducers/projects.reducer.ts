import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { Action, createReducer, on } from '@ngrx/store';
import { Project } from 'src/app/shared/models/project.model';
import * as ProjectsActions from '../actions/projects.actions';

export const projectsFeatureKey = 'projects';

export interface State extends EntityState<Project> {
  // additional state property
  loading: boolean;
  error: any;
  selectedProjectId: string;
}

export const adapter: EntityAdapter<Project> = createEntityAdapter<Project>();

export const initialState: State = adapter.getInitialState({
  // additional entity state properties
  loading: false,
  error: null,
  selectedProjectId: null,
});

export const reducer = createReducer(
  initialState,
  on(ProjectsActions.getProjects, (state) => {
    return {
      ...state,
      loading: true,
      error: null,
    };
  }),
  on(ProjectsActions.getProjectsSuccess, (state, { response }) => {
    return adapter.upsertMany(response.data, { ...state, loading: false });
  }),
  on(ProjectsActions.getProjectsFailure, (state, { error }) => {
    return {
      ...state,
      loading: false,
      error,
    };
  }),
  on(ProjectsActions.getProjectById, (state) => {
    return {
      ...state,
      loading: true,
      error: null,
    };
  }),
  on(ProjectsActions.getProjectByIdSuccess, (state, { response }) => {
    return adapter.upsertOne(response.data, { ...state, loading: false });
  }),
  on(ProjectsActions.getProjectByIdFailure, (state, { error }) => {
    return {
      ...state,
      loading: false,
      error,
    };
  }),
  on(ProjectsActions.selectProjectById, (state, { id }) => {
    return {
      ...state,
      selectedProjectId: id,
    };
  })
);

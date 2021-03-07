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
  on(ProjectsActions.loadProjects, (state) => {
    return {
      ...state,
      loading: true,
      error: null,
    };
  }),
  on(ProjectsActions.loadProjectsSuccess, (state, { response }) => {
    return adapter.setAll(response.data, { ...state, loading: false });
  }),
  on(ProjectsActions.loadProjectsFailure, (state, { error }) => {
    state = adapter.removeAll(state);
    return {
      ...state,
      loading: false,
      error,
    };
  }),
  // TODO: Load project by id
  on(ProjectsActions.selectProject, (state, { project }) => {
    return {
      ...state,
      selectedProjectId: project.id,
    };
  })
);

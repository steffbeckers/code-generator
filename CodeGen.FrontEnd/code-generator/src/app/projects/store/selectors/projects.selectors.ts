import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromProjects from '../reducers/projects.reducer';

export const selectProjectsState = createFeatureSelector<fromProjects.State>(
  fromProjects.projectsFeatureKey
);

// Added these custom selectors

export const {
  selectIds,
  selectEntities,
  selectAll,
  selectTotal,
} = fromProjects.adapter.getSelectors(selectProjectsState);

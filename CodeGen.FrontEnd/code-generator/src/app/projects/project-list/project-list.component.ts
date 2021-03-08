import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as ProjectsActions from 'src/app/projects/store/actions/projects.actions';
import { selectAll } from '../store/selectors/projects.selectors';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss'],
})
export class ProjectListComponent implements OnInit {
  projects$ = this.store.select(selectAll);

  constructor(private store: Store) {}

  ngOnInit(): void {
    this.store.dispatch(ProjectsActions.getProjects());
  }
}

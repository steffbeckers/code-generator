import { NgModule } from '@angular/core';
import { ProjectsComponent } from './projects.component';
import { ProjectsRoutingModule } from './projects-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';

@NgModule({
  declarations: [ProjectsComponent, ProjectListComponent, ProjectDetailComponent],
  imports: [SharedModule, ProjectsRoutingModule],
})
export class ProjectsModule {}

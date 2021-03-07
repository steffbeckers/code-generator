import { NgModule } from '@angular/core';
import { ProjectsComponent } from './projects.component';
import { ProjectsRoutingModule } from './projects-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectDetailComponent } from './project-detail/project-detail.component';
import { StoreModule } from '@ngrx/store';
import * as fromProjects from './store/reducers/projects.reducer';
import { EffectsModule } from '@ngrx/effects';
import { ProjectsEffects } from './store/effects/projects.effects';
import { ProjectsService } from '../shared/services/projects.service';

@NgModule({
  declarations: [
    ProjectsComponent,
    ProjectListComponent,
    ProjectDetailComponent,
  ],
  imports: [
    SharedModule,
    ProjectsRoutingModule,
    StoreModule.forFeature(
      fromProjects.projectsFeatureKey,
      fromProjects.reducer
    ),
    EffectsModule.forFeature([ProjectsEffects]),
  ],
  providers: [ProjectsService],
})
export class ProjectsModule {}

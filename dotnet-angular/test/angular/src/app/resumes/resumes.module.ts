import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResumesRoutingModule } from './resumes-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ResumesListComponent } from '../resumes/list/list.component';
import { ResumeDetailComponent } from '../resumes/detail/detail.component';
import { ResumeCreateComponent } from '../resumes/create/create.component';
import { ResumeUpdateComponent } from '../resumes/update/update.component';
import { ResumeLinkComponent } from '../resumes/link/link.component';

@NgModule({
  declarations: [
    ResumesListComponent,
    ResumeDetailComponent,
    ResumeCreateComponent,
    ResumeUpdateComponent,
    ResumeLinkComponent,
  ],
  imports: [CommonModule, ResumesRoutingModule, SharedModule]
})
export class ResumesModule {}

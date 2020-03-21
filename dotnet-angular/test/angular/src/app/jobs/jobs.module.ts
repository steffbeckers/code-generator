import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobsRoutingModule } from './jobs-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { JobsListComponent } from '../jobs/list/list.component';
import { JobDetailComponent } from '../jobs/detail/detail.component';
import { JobCreateComponent } from '../jobs/create/create.component';
import { JobUpdateComponent } from '../jobs/update/update.component';
import { JobLinkComponent } from '../jobs/link/link.component';

@NgModule({
  declarations: [
    JobsListComponent,
    JobDetailComponent,
    JobCreateComponent,
    JobUpdateComponent,
    JobLinkComponent,
  ],
  imports: [CommonModule, JobsRoutingModule, SharedModule]
})
export class JobsModule {}

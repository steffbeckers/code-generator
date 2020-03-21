import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JobStatesRoutingModule } from './jobstates-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { JobStatesListComponent } from '../jobstates/list/list.component';
import { JobStateDetailComponent } from '../jobstates/detail/detail.component';
import { JobStateCreateComponent } from '../jobstates/create/create.component';
import { JobStateUpdateComponent } from '../jobstates/update/update.component';
import { JobStateLinkComponent } from '../jobstates/link/link.component';

@NgModule({
  declarations: [
    JobStatesListComponent,
    JobStateDetailComponent,
    JobStateCreateComponent,
    JobStateUpdateComponent,
    JobStateLinkComponent,
  ],
  imports: [CommonModule, JobStatesRoutingModule, SharedModule]
})
export class JobStatesModule {}

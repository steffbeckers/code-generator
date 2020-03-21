import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResumeStatesRoutingModule } from './resumestates-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ResumeStatesListComponent } from '../resumestates/list/list.component';
import { ResumeStateDetailComponent } from '../resumestates/detail/detail.component';
import { ResumeStateCreateComponent } from '../resumestates/create/create.component';
import { ResumeStateUpdateComponent } from '../resumestates/update/update.component';
import { ResumeStateLinkComponent } from '../resumestates/link/link.component';

@NgModule({
  declarations: [
    ResumeStatesListComponent,
    ResumeStateDetailComponent,
    ResumeStateCreateComponent,
    ResumeStateUpdateComponent,
    ResumeStateLinkComponent,
  ],
  imports: [CommonModule, ResumeStatesRoutingModule, SharedModule]
})
export class ResumeStatesModule {}

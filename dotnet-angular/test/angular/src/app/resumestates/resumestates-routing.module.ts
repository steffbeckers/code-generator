import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ResumeStatesListComponent } from './list/list.component';
import { ResumeStateDetailComponent } from './detail/detail.component';
import { ResumeStateCreateComponent } from './create/create.component';
import { ResumeStateUpdateComponent } from './update/update.component';
import { ResumeStateLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: ResumeStateCreateComponent
  },
  {
    path: ':id/link/:model',
    component: ResumeStateLinkComponent
  },
  {
    path: ':id/edit',
    component: ResumeStateUpdateComponent
  },
  {
    path: ':id',
    component: ResumeStateDetailComponent
  },
  {
    path: '',
    component: ResumeStatesListComponent
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResumeStatesRoutingModule {}

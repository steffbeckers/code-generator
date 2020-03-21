import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { JobStatesListComponent } from './list/list.component';
import { JobStateDetailComponent } from './detail/detail.component';
import { JobStateCreateComponent } from './create/create.component';
import { JobStateUpdateComponent } from './update/update.component';
import { JobStateLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: JobStateCreateComponent
  },
  {
    path: ':id/link/:model',
    component: JobStateLinkComponent
  },
  {
    path: ':id/edit',
    component: JobStateUpdateComponent
  },
  {
    path: ':id',
    component: JobStateDetailComponent
  },
  {
    path: '',
    component: JobStatesListComponent
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
export class JobStatesRoutingModule {}

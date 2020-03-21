import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { JobsListComponent } from './list/list.component';
import { JobDetailComponent } from './detail/detail.component';
import { JobCreateComponent } from './create/create.component';
import { JobUpdateComponent } from './update/update.component';
import { JobLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: JobCreateComponent
  },
  {
    path: ':id/link/:model',
    component: JobLinkComponent
  },
  {
    path: ':id/edit',
    component: JobUpdateComponent
  },
  {
    path: ':id',
    component: JobDetailComponent
  },
  {
    path: '',
    component: JobsListComponent
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
export class JobsRoutingModule {}

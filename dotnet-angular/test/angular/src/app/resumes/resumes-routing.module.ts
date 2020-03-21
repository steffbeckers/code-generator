import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { ResumesListComponent } from './list/list.component';
import { ResumeDetailComponent } from './detail/detail.component';
import { ResumeCreateComponent } from './create/create.component';
import { ResumeUpdateComponent } from './update/update.component';
import { ResumeLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: ResumeCreateComponent
  },
  {
    path: ':id/link/:model',
    component: ResumeLinkComponent
  },
  {
    path: ':id/edit',
    component: ResumeUpdateComponent
  },
  {
    path: ':id',
    component: ResumeDetailComponent
  },
  {
    path: '',
    component: ResumesListComponent
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
export class ResumesRoutingModule {}

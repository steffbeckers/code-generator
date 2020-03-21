import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { SkillsListComponent } from './list/list.component';
import { SkillDetailComponent } from './detail/detail.component';
import { SkillCreateComponent } from './create/create.component';
import { SkillUpdateComponent } from './update/update.component';
import { SkillLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: SkillCreateComponent
  },
  {
    path: ':id/link/:model',
    component: SkillLinkComponent
  },
  {
    path: ':id/edit',
    component: SkillUpdateComponent
  },
  {
    path: ':id',
    component: SkillDetailComponent
  },
  {
    path: '',
    component: SkillsListComponent
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
export class SkillsRoutingModule {}

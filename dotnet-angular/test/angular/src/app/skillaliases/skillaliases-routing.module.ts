import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { SkillAliasesListComponent } from './list/list.component';
import { SkillAliasDetailComponent } from './detail/detail.component';
import { SkillAliasCreateComponent } from './create/create.component';
import { SkillAliasUpdateComponent } from './update/update.component';
import { SkillAliasLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: SkillAliasCreateComponent
  },
  {
    path: ':id/link/:model',
    component: SkillAliasLinkComponent
  },
  {
    path: ':id/edit',
    component: SkillAliasUpdateComponent
  },
  {
    path: ':id',
    component: SkillAliasDetailComponent
  },
  {
    path: '',
    component: SkillAliasesListComponent
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
export class SkillAliasesRoutingModule {}

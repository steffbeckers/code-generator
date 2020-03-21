import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SkillAliasesRoutingModule } from './skillaliases-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { SkillAliasesListComponent } from '../skillaliases/list/list.component';
import { SkillAliasDetailComponent } from '../skillaliases/detail/detail.component';
import { SkillAliasCreateComponent } from '../skillaliases/create/create.component';
import { SkillAliasUpdateComponent } from '../skillaliases/update/update.component';
import { SkillAliasLinkComponent } from '../skillaliases/link/link.component';

@NgModule({
  declarations: [
    SkillAliasesListComponent,
    SkillAliasDetailComponent,
    SkillAliasCreateComponent,
    SkillAliasUpdateComponent,
    SkillAliasLinkComponent,
  ],
  imports: [CommonModule, SkillAliasesRoutingModule, SharedModule]
})
export class SkillAliasesModule {}

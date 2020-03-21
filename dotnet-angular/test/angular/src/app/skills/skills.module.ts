import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SkillsRoutingModule } from './skills-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { SkillsListComponent } from '../skills/list/list.component';
import { SkillDetailComponent } from '../skills/detail/detail.component';
import { SkillCreateComponent } from '../skills/create/create.component';
import { SkillUpdateComponent } from '../skills/update/update.component';
import { SkillLinkComponent } from '../skills/link/link.component';

@NgModule({
  declarations: [
    SkillsListComponent,
    SkillDetailComponent,
    SkillCreateComponent,
    SkillUpdateComponent,
    SkillLinkComponent,
  ],
  imports: [CommonModule, SkillsRoutingModule, SharedModule]
})
export class SkillsModule {}

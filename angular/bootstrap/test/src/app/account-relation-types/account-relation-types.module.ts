import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { AccountRelationTypeService } from './account-relation-type.service';

// Modules
import { AccountRelationTypesRoutingModule } from './account-relation-types-routing.module';

// Components
import { AccountRelationTypesComponent } from './account-relation-types.component';
import { CreateAccountRelationTypeComponent } from './create-account-relation-type/create-account-relation-type.component';
import { DetailAccountRelationTypeComponent } from './detail-account-relation-type/detail-account-relation-type.component';
import { EditAccountRelationTypeComponent } from './edit-account-relation-type/edit-account-relation-type.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AccountRelationTypesComponent,
    CreateAccountRelationTypeComponent,
    DetailAccountRelationTypeComponent,
    EditAccountRelationTypeComponent
  ],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, AccountRelationTypesRoutingModule],
  providers: [AccountRelationTypeService]
})
export class AccountRelationTypesModule {}

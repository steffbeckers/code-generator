import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsRoutingModule } from './contacts-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ContactsListComponent } from '../contacts/list/list.component';
import { ContactDetailComponent } from '../contacts/detail/detail.component';
import { ContactCreateComponent } from '../contacts/create/create.component';
import { ContactUpdateComponent } from '../contacts/update/update.component';

@NgModule({
  declarations: [
    ContactsListComponent,
    ContactDetailComponent,
    ContactCreateComponent,
    ContactUpdateComponent,
  ],
  imports: [CommonModule, ContactsRoutingModule, SharedModule]
})
export class ContactsModule {}

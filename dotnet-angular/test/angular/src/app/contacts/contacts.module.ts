import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactsRoutingModule } from './contacts-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ContactsListComponent } from '../contacts/list/list.component';
import { ContactCreateComponent } from '../contacts/create/create.component';

@NgModule({
  declarations: [
    ContactsListComponent,
    ContactCreateComponent,
  ],
  imports: [CommonModule, ContactsRoutingModule, SharedModule]
})
export class ContactsModule {}

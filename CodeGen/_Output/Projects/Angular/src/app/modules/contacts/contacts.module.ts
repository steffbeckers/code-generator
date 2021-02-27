import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { ContactsRoutingModule } from './contacts-routing.module';
import { ContactsComponent } from './contacts.component';

@NgModule({
  declarations: [ContactsComponent],
  imports: [SharedModule, ContactsRoutingModule],
})
export class ContactsModule {}

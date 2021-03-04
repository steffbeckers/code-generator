import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { ContactsRoutingModule } from './contacts-routing.module';
import { ContactsComponent } from './contacts.component';
import { ContactsService } from './contacts.service';
import { ContactsListComponent } from './list/list.component';
import { ContactsDetailComponent } from './detail/detail.component';
import { ContactsCreateComponent } from './create/create.component';
import { ContactsEditComponent } from './edit/edit.component';

@NgModule({
  declarations: [
    ContactsComponent,
    ContactsListComponent,
    ContactsDetailComponent,
    ContactsCreateComponent,
    ContactsEditComponent,
  ],
  imports: [SharedModule, ContactsRoutingModule],
  providers: [ContactsService],
})
export class ContactsModule {}

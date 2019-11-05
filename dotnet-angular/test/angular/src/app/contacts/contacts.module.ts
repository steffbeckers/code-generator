import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ContactsRoutingModule } from './contacts-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { ContactsListComponent } from '../contacts/list/list.component';

@NgModule({
  declarations: [
    ContactsListComponent,
  ],
  imports: [CommonModule, ContactsRoutingModule, SharedModule]
})
export class ContactsModule {}

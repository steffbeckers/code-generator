import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { ContactService } from './contact.service';

// Modules
import { ContactsRoutingModule } from './contacts-routing.module';

// Components
import { ContactsComponent } from './contacts.component';
import { CreateContactComponent } from './create-contact/create-contact.component';
import { DetailContactComponent } from './detail-contact/detail-contact.component';
import { EditContactComponent } from './edit-contact/edit-contact.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [ContactsComponent, CreateContactComponent, DetailContactComponent, EditContactComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, ContactsRoutingModule],
  providers: [ContactService]
})
export class ContactsModule {}

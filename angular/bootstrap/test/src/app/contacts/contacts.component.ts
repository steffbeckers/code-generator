import { Component, OnInit } from '@angular/core';

// Models
import { Contact } from './contact';

// Services
import { ContactService } from './contact.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})
export class ContactsComponent implements OnInit {
  contacts: Contact[];

  constructor(private contactService: ContactService) {}

  ngOnInit() {
    this.contactService.getAll().subscribe((contacts: Contact[]) => {
      this.contacts = contacts;
    });
  }
}

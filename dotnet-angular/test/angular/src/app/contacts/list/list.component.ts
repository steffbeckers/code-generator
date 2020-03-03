import { Component, OnInit } from '@angular/core';

// Models
import { Contact } from 'src/app/shared/models/Contact';

// Services
import { ContactService } from 'src/app/shared/services/ContactService';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ContactsListComponent implements OnInit {
  public contacts: Contact[];

  constructor(private contactService: ContactService) {
    this.contacts = null;
  }

  ngOnInit(): void {
    this.getContacts();
  }

  private getContacts(): void {
    this.contactService.getContacts().subscribe(
      (contacts: Contact[]) => {
        this.contacts = contacts;
      }
    );
  }
}

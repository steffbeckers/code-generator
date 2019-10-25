import { Component, OnInit } from './node_modules/@angular/core';
import { ActivatedRoute } from './node_modules/@angular/router';

// Services
import { ContactService } from '../__entityName@dasherize__.service';

// Models
import { Contact } from '../__entityName@dasherize__';

@Component({
  selector: 'app-contacts-detail',
  templateUrl: './detail-contact.component.html',
  styleUrls: ['./detail-contact.component.scss']
})
export class DetailContactComponent implements OnInit {
  contact: Contact;

  constructor(private route: ActivatedRoute, private contactService: ContactService) {}

  ngOnInit() {
    this.getContact();
  }

  getContact() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.contactService.getById(id).subscribe((contact: Contact) => {
        this.contact = contact;
      });
    }
  }
}

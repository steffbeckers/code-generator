import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Contact } from 'src/app/shared/models/Contact';

// Services
import { ContactService } from 'src/app/shared/services/ContactService';

@Component({
  selector: 'app-contact-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class ContactDetailComponent implements OnInit {
  // Contact
  public contact: Contact;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private contactService: ContactService
  ) {
    this.contact = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getContact(routeParams.id);
    });
  }

  private getContact(id: string): void {
    this.contactService.getContact(id).subscribe(
      (contact: Contact) => {
        this.contact = contact;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Contact could not be found.');
          this.router.navigateByUrl('/contacts');
        }
      }
    );
  }

  public deleteContact(): void {
    // Validate
    if (!this.contact && !this.contact.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete contact: ' + this.contact.firstName + '?')) {
      this.contactService.deleteContact(this.contact.id).subscribe(
        () => {
          this.router.navigateByUrl('/contacts');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Contact could not be found.');
            this.router.navigateByUrl('/contacts');
          }
        }
      );
    }
  }
}

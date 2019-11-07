import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Contact } from 'src/app/shared/models/Contact';

// Services
import { ContactService } from 'src/app/shared/services/ContactService';

@Component({
  selector: 'app-contact-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class ContactUpdateComponent implements OnInit {
  // Contact
  public contact: Contact;
  public contactForm: FormGroup;
  public updating = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private contactService: ContactService
  ) {}

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getContact(routeParams.id);
    });

    this.contactForm = this.fb.group({
      id: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      website: [''],
      telephone: [''],
      email: [''],
    });
  }

  private getContact(id: string): void {
    this.contactService.getContact(id).subscribe(
      (contact: Contact) => {
        this.contact = contact;
        this.contactForm.patchValue(this.contact);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Contact could not be found.');
          this.router.navigateByUrl('/contacts');
        }
      }
    );
  }

  public updateContact(andClose: boolean = false): void {
    // Validate
    if (this.contactForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.contactForm.pristine && andClose) {
      this.router.navigateByUrl('/contacts/' + this.contact.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.contactService.updateContact(this.contactForm.value).subscribe(
      (contact: Contact) => {
        if (andClose) {
          this.router.navigateByUrl('/contacts/' + contact.id);
        }

        this.contact = contact;
        this.contactForm.patchValue(this.contact);
      },
      null,
      () => {
        this.updating = false;
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

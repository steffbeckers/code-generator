import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Contact } from 'src/app/shared/models/Contact';

// Services
import { ContactService } from 'src/app/shared/services/ContactService';

@Component({
  selector: 'app-contact-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class ContactCreateComponent implements OnInit {
  // Contact
  public contactForm: FormGroup;
  public creating = false;

  constructor(
    private router: Router,
    public fb: FormBuilder,
    public contactService: ContactService
  ) {}

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      website: [''],
      telephone: [''],
      email: [''],
    });
  }

  public createContact(): void {
    // Validate
    if (this.contactForm.invalid || this.creating) {
      return;
    }
    this.creating = true;

    this.contactService.createContact(this.contactForm.value).subscribe(
      (contact: Contact) => {
        this.router.navigateByUrl('/contacts/' + contact.id);
      }
    );
  }
}

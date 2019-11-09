import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

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
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private contactService: ContactService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.contactForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      website: [''],
      telephone: [''],
      email: [''],
      accountId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.contactForm.patchValue(queryParams);
    });
  }

  public createContact(): void {
    // Validate
    if (this.contactForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.contactService.createContact(this.contactForm.value).subscribe(
      (contact: Contact) => {
        this.creating = false;

        this.router.navigateByUrl('/contacts/' + contact.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

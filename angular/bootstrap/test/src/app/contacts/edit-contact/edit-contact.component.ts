import { Component, OnInit } from "./node_modules/@angular/core";
import { ActivatedRoute, Router } from "./node_modules/@angular/router";
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl
} from "./node_modules/@angular/forms";

// RxJS
import { Observable, of } from "./node_modules/rxjs";
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  switchMap
} from "./node_modules/rxjs/operators";

// Services
import { ContactService } from "../__entityName@dasherize__.service";

// Models
import { Contact } from "../__entityName@dasherize__";

// Validators
function notRequiredEmailValidator(
  control: AbstractControl
): { [key: string]: any } {
  if (!control.value) {
    return null;
  }
  return Validators.email(control);
}

@Component({
  selector: "app-contacts-edit",
  templateUrl: "./edit-contact.component.html",
  styleUrls: ["./edit-contact.component.scss"]
})
export class EditContactComponent implements OnInit {
  contact: Contact;
  contactForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private contactService: ContactService
  ) {}

  searchContact = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.contactService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterContact = (foundContact: Contact) => {
    return foundContact.name;
  };

  ngOnInit() {
    this.contactForm = this.fb.group({
      id: [""],
      name: ["", [Validators.required, Validators.maxLength(50)]],
      telephone: ["", Validators.maxLength(50)],
      fax: ["", Validators.maxLength(50)],
      email: ["", [notRequiredEmailValidator, Validators.maxLength(100)]],
      website: ["", Validators.maxLength(200)],
      vatNumber: ["", Validators.maxLength(50)],
      description: ["", Validators.maxLength(4000)],
      address: this.fb.group({
        id: [""],
        street: ["", Validators.maxLength(250)],
        number: ["", Validators.maxLength(50)],
        postalcode: ["", Validators.maxLength(10)],
        city: ["", Validators.maxLength(80)],
        state: ["", Validators.maxLength(50)],
        country: "",
        latitude: [0, [Validators.min(-90), Validators.max(90)]],
        longitude: [0, [Validators.min(-180), Validators.max(180)]]
      }),
      parentContactId: [""],
      billingContactId: [""]
    });

    this.getContact();
  }

  getContact() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.contactService.getById(id).subscribe((contact: Contact) => {
        this.contact = contact;

        // Patch the contact into the form
        this.contactForm.patchValue(this.contact);
      });
    }
  }

  editContact() {
    // Validate
    if (this.contactForm.invalid) {
      return;
    }

    this.contactService
      .update(this.contact.id, this.contactForm.value)
      .subscribe(() => {
        this.router.navigate(["/contacts", this.contact.id]);
      });
  }
}

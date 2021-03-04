import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Contact } from 'src/app/shared/models/contact.model';
import { Response } from 'src/app/shared/models/response.model';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-contacts-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class ContactsCreateComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  saving: boolean;
  form = this.fb.group({
    name: [null, [Validators.required]],
    description: [null],
    telephone: [null],
    email: [null],
    website: [null],
  });

  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  save(): void {
    if (this.saving || this.form.invalid) {
      return;
    }

    const contact: Contact = this.form.getRawValue();

    this.saving = true;
    this.subs.push(
      this.contactsService.createContact(contact).subscribe(
        (response: Response) => {
          this.saving = false;

          if (!response.success) {
            return;
          }

          this.router.navigateByUrl(`/contacts/${response.data.id}`);
        },
        (error: any) => {
          this.saving = false;
        }
      )
    );
  }
}

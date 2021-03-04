import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Contact } from 'src/app/shared/models/contact.model';
import { Response } from 'src/app/shared/models/response.model';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-contacts-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class ContactsEditComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  contact$: BehaviorSubject<Contact> = new BehaviorSubject<Contact>(null);
  saving: boolean;
  close: boolean;
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
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.contactsService
            .getContactById(id)
            .subscribe((response: Response) => {
              if (!response.success) {
                // TODO: Check code
                this.router.navigateByUrl('/contacts');
                return;
              }

              this.contact$.next(response.data);
            })
        );
      })
    );

    this.subs.push(
      this.contact$.subscribe((contact: Contact) => {
        this.form.patchValue(contact);
        this.form.markAsPristine();
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  save(): void {
    if (this.saving || this.form.invalid) {
      return;
    }

    if (this.form.pristine) {
      if (this.close) {
        this.router.navigateByUrl(`/contacts/${this.contact$.value.id}`);
        return;
      }
      return;
    }

    const contact: Contact = {
      ...this.contact$.value,
      ...this.form.getRawValue(),
    };

    this.saving = true;
    this.subs.push(
      this.contactsService.updateContact(contact).subscribe(
        (response: Response) => {
          this.saving = false;

          if (!response.success) {
            return;
          }

          if (this.close) {
            this.router.navigateByUrl(`/contacts/${contact.id}`);
            return;
          }

          this.contact$.next(response.data);
        },
        (error: any) => {
          this.saving = false;
        }
      )
    );
  }
}

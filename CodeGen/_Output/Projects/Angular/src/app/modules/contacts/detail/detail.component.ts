import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Contact } from 'src/app/shared/models/contact.model';
import { Response } from 'src/app/shared/models/response.model';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-contacts-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class ContactsDetailComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  contact$: BehaviorSubject<Contact> = new BehaviorSubject<Contact>(null);

  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.contactsService
            .getContactById(id, 'Contacts')
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
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  delete(): void {
    if (confirm('Are you sure?')) {
      const contact: Contact = this.contact$.value;
      this.subs.push(
        this.contactsService
          .deleteContact(contact)
          .subscribe((response: Response) => {
            if (response.success) {
              this.router.navigateByUrl('/contacts');
            }
          })
      );
    }
  }
}

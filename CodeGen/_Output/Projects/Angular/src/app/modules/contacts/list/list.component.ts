import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Contact } from 'src/app/shared/models/account.model';
import { Response } from 'src/app/shared/models/response.model';
import { ContactsService } from '../contacts.service';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ContactsListComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  contacts$: BehaviorSubject<Contact[]> = new BehaviorSubject<Contact[]>(null);

  constructor(private contactsService: ContactsService) {}

  ngOnInit(): void {
    this.subs.push(
      this.contactsService.getContacts().subscribe((response: Response) => {
        this.contacts$.next(response.data);
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }
}

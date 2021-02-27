import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Account } from 'src/app/shared/models/account.model';
import { Response } from 'src/app/shared/models/response.model';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-accounts-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class AccountsListComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  accounts$: BehaviorSubject<Account[]> = new BehaviorSubject<Account[]>(null);

  constructor(private accountsService: AccountsService) {}

  ngOnInit(): void {
    this.subs.push(
      this.accountsService.getAccounts().subscribe((response: Response) => {
        this.accounts$.next(response.data);
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }
}

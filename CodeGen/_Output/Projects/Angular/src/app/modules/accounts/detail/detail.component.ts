import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Account } from 'src/app/shared/models/account.model';
import { Response } from 'src/app/shared/models/response.model';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-accounts-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
})
export class AccountsDetailComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  account$: BehaviorSubject<Account> = new BehaviorSubject<Account>(null);

  constructor(
    private accountsService: AccountsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.accountsService
            .getAccountById(id, 'Contacts')
            .subscribe((response: Response) => {
              if (!response.success) {
                // TODO: Check code
                this.router.navigateByUrl('/accounts');
                return;
              }

              this.account$.next(response.data);
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
      const account: Account = this.account$.value;
      this.subs.push(
        this.accountsService
          .deleteAccount(account)
          .subscribe((response: Response) => {
            if (response.success) {
              this.router.navigateByUrl('/accounts');
            }
          })
      );
    }
  }
}
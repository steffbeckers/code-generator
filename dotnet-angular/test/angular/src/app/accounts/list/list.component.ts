import { Component, OnInit } from '@angular/core';

// Models
import { Account } from 'src/app/shared/models/Account';

// Services
import { AccountService } from 'src/app/shared/services/AccountService';

@Component({
  selector: 'app-accounts-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AccountsListComponent implements OnInit {
  public accounts: Account[];

  constructor(private accountService: AccountService) {
    this.accounts = null;
  }

  ngOnInit(): void {
    this.getAccounts();
  }

  private getAccounts(): void {
    this.accountService.getAccounts().subscribe(
      (accounts: Account[]) => {
        this.accounts = accounts;
      }
    );
  }
}

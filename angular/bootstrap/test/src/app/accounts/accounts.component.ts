import { Component, OnInit } from '@angular/core';

// Models
import { Account } from './account';

// Services
import { AccountService } from './account.service';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.scss']
})
export class AccountsComponent implements OnInit {
  accounts: Account[];

  constructor(private accountService: AccountService) {}

  ngOnInit() {
    this.accountService.getAll().subscribe((accounts: Account[]) => {
      this.accounts = accounts;
    });
  }
}

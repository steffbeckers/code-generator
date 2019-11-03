import { Component, OnInit } from '@angular/core';

// Models
import { Account } from './shared/models/Account';

// Services
import { AccountService } from './shared/services/AccountService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'test-app';
  accounts: Account[];

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.getAccounts();
  }

  private getAccounts() {
    this.accountService.getAccounts().subscribe((accounts: Account[]) => {
      this.accounts = accounts;
    });
  }
}

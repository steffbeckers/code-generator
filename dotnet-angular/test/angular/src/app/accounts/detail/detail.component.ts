import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Account } from 'src/app/shared/models/Account';

// Services
import { AccountService } from 'src/app/shared/services/AccountService';

@Component({
  selector: 'app-account-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class AccountDetailComponent implements OnInit {
  // Account
  public account: Account;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private accountService: AccountService
  ) {
    this.account = new Account();
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getAccount(routeParams.id);
    });
  }

  private getAccount(id: string): void {
    this.accountService.getAccount(id).subscribe(
      (account: Account) => {
        this.account = account;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Account could not be found.');
          this.router.navigateByUrl('/accounts');
        }
      }
    );
  }

  public deleteAccount(): void {
    // Validate
    if (!this.account && !this.account.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete account: ' + this.account.name + '?')) {
      this.accountService.deleteAccount(this.account.id).subscribe(
        () => {
          this.router.navigateByUrl('/accounts');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Account could not be found.');
            this.router.navigateByUrl('/accounts');
          }
        }
      );
    }
  }
}

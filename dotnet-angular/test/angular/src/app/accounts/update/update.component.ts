import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Account } from 'src/app/shared/models/Account';

// Services
import { AccountService } from 'src/app/shared/services/AccountService';

@Component({
  selector: 'app-account-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class AccountUpdateComponent implements OnInit {
  // Account
  public account: Account;
  public accountForm: FormGroup;
  public updating = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getAccount(routeParams.id);
    });

    this.accountForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      website: [''],
      telephone: [''],
      email: [''],
    });
  }

  private getAccount(id: string): void {
    this.accountService.getAccount(id).subscribe(
      (account: Account) => {
        this.account = account;
        this.accountForm.patchValue(this.account);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Account could not be found.');
          this.router.navigateByUrl('/accounts');
        }
      }
    );
  }

  public updateAccount(andClose: boolean = false): void {
    // Validate
    if (this.accountForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.accountForm.pristine && andClose) {
      this.router.navigateByUrl('/accounts/' + this.account.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.accountService.updateAccount(this.accountForm.value).subscribe(
      (account: Account) => {
        if (andClose) {
          this.router.navigateByUrl('/accounts/' + account.id);
        }

        this.account = account;
        this.accountForm.patchValue(this.account);
      },
      null,
      () => {
        this.updating = false;
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

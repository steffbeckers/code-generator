import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Account } from 'src/app/shared/models/Account';

// Services
import { AccountService } from 'src/app/shared/services/AccountService';

@Component({
  selector: 'app-account-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class AccountCreateComponent implements OnInit {
  // Account
  public accountForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private accountService: AccountService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.accountForm = this.fb.group({
      name: ['', Validators.required],
      website: [''],
      telephone: [''],
      email: [''],
    });
  }

  public createAccount(): void {
    // Validate
    if (this.accountForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.accountService.createAccount(this.accountForm.value).subscribe(
      (account: Account) => {
        this.creating = false;

        this.router.navigateByUrl('/accounts/' + account.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

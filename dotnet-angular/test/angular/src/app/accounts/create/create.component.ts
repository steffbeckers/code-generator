import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

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
  public creating = false;

  constructor(
    private router: Router,
    public fb: FormBuilder,
    public accountService: AccountService
  ) {}

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
    this.creating = true;

    this.accountService.createAccount(this.accountForm.value).subscribe(
      () => {
        this.router.navigateByUrl('/accounts');
      }
    );
  }
}

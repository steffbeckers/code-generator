import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Account } from 'src/app/shared/models/account.model';
import { Response } from 'src/app/shared/models/response.model';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-accounts-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
})
export class AccountsCreateComponent implements OnInit, OnDestroy {
  private subs: Subscription[] = [];

  saving: boolean;
  form = this.fb.group({
    name: [null, [Validators.required]],
    description: [null],
    telephone: [null],
    email: [null],
    website: [null],
  });

  constructor(
    private accountsService: AccountsService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  save(): void {
    if (this.saving || this.form.invalid) {
      return;
    }

    const account: Account = this.form.getRawValue();

    this.saving = true;
    this.subs.push(
      this.accountsService.createAccount(account).subscribe(
        (response: Response) => {
          this.saving = false;

          if (!response.success) {
            return;
          }

          this.router.navigateByUrl(`/accounts/${response.data.id}`);
        },
        (error: any) => {
          this.saving = false;
        }
      )
    );
  }
}

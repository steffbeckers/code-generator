import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Subscription } from 'rxjs';
import { Response } from 'src/app/shared/models/response.model';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-accounts-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss'],
})
export class AccountsEditComponent implements OnInit {
  private subs: Subscription[] = [];

  account$: BehaviorSubject<Account> = new BehaviorSubject<Account>(null);
  saving: boolean;
  close: boolean;
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
    private route: ActivatedRoute,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.subs.push(
      this.route.paramMap.subscribe((paramMap: any) => {
        const id: string = paramMap.params.id;

        this.subs.push(
          this.accountsService
            .getAccountById(id)
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

    this.subs.push(
      this.account$.subscribe((account: Account) => {
        this.form.patchValue(account);
        this.form.markAsPristine();
      })
    );
  }

  ngOnDestroy(): void {
    for (const sub of this.subs) {
      sub.unsubscribe();
    }
  }

  save(): void {
    if (this.saving || this.form.invalid) {
      return;
    }

    const account: Account = {
      ...this.account$.value,
      ...this.form.getRawValue(),
    };

    this.saving = true;
    this.subs.push(
      this.accountsService.updateAccount(account).subscribe(
        (response: Response) => {
          this.saving = false;

          if (!response.success) {
            return;
          }

          if (this.close) {
            this.router.navigateByUrl(`/accounts/${account.id}`);
            return;
          }

          this.account$.next(response.data);
        },
        (error: any) => {
          this.saving = false;
        }
      )
    );
  }
}

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';

// RxJS
import { Observable, of } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';

// Services
import { AccountService } from '../account.service';

// Models
import { Account } from '../account';

// Validators
function notRequiredEmailValidator(control: AbstractControl): { [key: string]: any } {
  if (!control.value) {
    return null;
  }
  return Validators.email(control);
}

@Component({
  selector: 'app-accounts-edit',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss']
})
export class EditAccountComponent implements OnInit {
  account: Account;
  accountForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private accountService: AccountService
  ) {}

  searchAccount = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.accountService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterAccount = (foundAccount: Account) => {
    return foundAccount.name;
  };

  ngOnInit() {
    this.accountForm = this.fb.group({
      id: [''],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      telephone: ['', Validators.maxLength(50)],
      fax: ['', Validators.maxLength(50)],
      email: ['', [notRequiredEmailValidator, Validators.maxLength(100)]],
      website: ['', Validators.maxLength(200)],
      vatNumber: ['', Validators.maxLength(50)],
      description: ['', Validators.maxLength(4000)],
      address: this.fb.group({
        id: [''],
        street: ['', Validators.maxLength(250)],
        number: ['', Validators.maxLength(50)],
        postalcode: ['', Validators.maxLength(10)],
        city: ['', Validators.maxLength(80)],
        state: ['', Validators.maxLength(50)],
        country: '',
        latitude: [0, [Validators.min(-90), Validators.max(90)]],
        longitude: [0, [Validators.min(-180), Validators.max(180)]]
      }),
      parentAccountId: [''],
      billingAccountId: ['']
    });

    this.getAccount();
  }

  getAccount() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.accountService.getById(id).subscribe((account: Account) => {
        this.account = account;

        // Patch the account into the form
        this.accountForm.patchValue(this.account);
      });
    }
  }

  editAccount() {
    // Validate
    if (this.accountForm.invalid) {
      return;
    }

    this.accountService.update(this.account.id, this.accountForm.value).subscribe(() => {
      this.router.navigate(['/accounts', this.account.id]);
    });
  }
}

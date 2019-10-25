import { Component, OnInit } from './node_modules/@angular/core';
import { ActivatedRoute, Router } from './node_modules/@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from './node_modules/@angular/forms';

// RxJS
import { Observable, of } from './node_modules/rxjs';
import { catchError, debounceTime, distinctUntilChanged, switchMap } from './node_modules/rxjs/operators';

// Services
import { AddressService } from '../__entityName@dasherize__.service';

// Models
import { Address } from '../__entityName@dasherize__';

// Validators
function notRequiredEmailValidator(control: AbstractControl): { [key: string]: any } {
  if (!control.value) {
    return null;
  }
  return Validators.email(control);
}

@Component({
  selector: 'app-addresses-edit',
  templateUrl: './edit-address.component.html',
  styleUrls: ['./edit-address.component.scss']
})
export class EditAddressComponent implements OnInit {
  address: Address;
  addressForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private addressService: AddressService
  ) {}

  searchAddress = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.addressService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterAddress = (foundAddress: Address) => {
    return foundAddress.name;
  };

  ngOnInit() {
    this.addressForm = this.fb.group({
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
      parentAddressId: [''],
      billingAddressId: ['']
    });

    this.getAddress();
  }

  getAddress() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.addressService.getById(id).subscribe((address: Address) => {
        this.address = address;

        // Patch the address into the form
        this.addressForm.patchValue(this.address);
      });
    }
  }

  editAddress() {
    // Validate
    if (this.addressForm.invalid) {
      return;
    }

    this.addressService.update(this.address.id, this.addressForm.value).subscribe(() => {
      this.router.navigate(['/addresses', this.address.id]);
    });
  }
}

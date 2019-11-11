import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Address } from 'src/app/shared/models/Address';

// Services
import { AddressService } from 'src/app/shared/services/AddressService';

@Component({
  selector: 'app-address-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class AddressCreateComponent implements OnInit {
  // Address
  public addressForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private addressService: AddressService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.addressForm = this.fb.group({
      street: ['', Validators.required],
      number: ['', Validators.required],
      postalCode: ['', Validators.required],
      city: ['', Validators.required],
      primary: [''],
      accountId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.addressForm.patchValue(queryParams);
    });
  }

  public createAddress(): void {
    // Validate
    if (this.addressForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.addressService.createAddress(this.addressForm.value).subscribe(
      (address: Address) => {
        this.creating = false;

        this.router.navigateByUrl('/addresses/' + address.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

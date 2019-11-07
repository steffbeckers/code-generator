import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

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
    private fb: FormBuilder,
    private addressService: AddressService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.addressForm = this.fb.group({
      street: [''],
      number: [''],
      postalCode: [''],
      city: [''],
      primary: [''],
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

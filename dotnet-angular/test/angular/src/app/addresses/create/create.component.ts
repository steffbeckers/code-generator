import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

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
  public creating = false;

  constructor(
    private router: Router,
    public fb: FormBuilder,
    public addressService: AddressService
  ) {}

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
    this.creating = true;

    this.addressService.createAddress(this.addressForm.value).subscribe(
      () => {
        this.router.navigateByUrl('/addresses');
      }
    );
  }
}

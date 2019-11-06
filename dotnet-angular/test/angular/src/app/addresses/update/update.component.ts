import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Address } from 'src/app/shared/models/Address';

// Services
import { AddressService } from 'src/app/shared/services/AddressService';

@Component({
  selector: 'app-address-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class AddressUpdateComponent implements OnInit {
  // Address
  public address: Address;
  public addressForm: FormGroup;
  public updating = false;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private addressService: AddressService
  ) {}

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getAddress(routeParams.id);
    });

    this.addressForm = this.fb.group({
      id: ['', Validators.required],
      street: [''],
      number: [''],
      postalCode: [''],
      city: [''],
      primary: [''],
    });
  }

  private getAddress(id: string): void {
    this.addressService.getAddress(id).subscribe(
      (address: Address) => {
        this.address = address;
        this.addressForm.patchValue(this.address);
      }
    );
  }

  public updateAddress(andClose: boolean = false): void {
    // Validate
    if (this.addressForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.addressForm.pristine && andClose) {
      this.router.navigateByUrl('/addresses/' + this.address.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.addressService.updateAddress(this.addressForm.value).subscribe(
      (address: Address) => {
        this.updating = false;

        if (andClose) {
          this.router.navigateByUrl('/addresses/' + address.id);
        }

        this.address = address;
        this.addressForm.patchValue(this.address);
      }
    );
  }

  public deleteAddress(): void {
    // Validate
    if (!this.address && !this.address.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete address: ' + this.address.street + '?')) {
      this.addressService.deleteAddress(this.address.id).subscribe(
        () => {
          this.router.navigateByUrl('/addresses');
        }
      );
    }
  }
}

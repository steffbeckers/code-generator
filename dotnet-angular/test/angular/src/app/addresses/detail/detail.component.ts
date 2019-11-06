import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Address } from 'src/app/shared/models/Address';

// Services
import { AddressService } from 'src/app/shared/services/AddressService';

@Component({
  selector: 'app-address-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class AddressDetailComponent implements OnInit {
  // Address
  public address: Address;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private addressService: AddressService
  ) {
    this.address = new Address();
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getAddress(routeParams.id);
    });
  }

  private getAddress(id: string): void {
    this.addressService.getAddress(id).subscribe(
      (address: Address) => {
        this.address = address;
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

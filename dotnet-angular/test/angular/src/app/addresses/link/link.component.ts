import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Address } from 'src/app/shared/models/Address';

// Services
import { AddressService } from 'src/app/shared/services/AddressService';

@Component({
  selector: 'app-address-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class AddressLinkComponent implements OnInit {
  // Address
  public address: Address;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private addressService: AddressService
  ) {
    this.address = null;
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
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Address could not be found.');
          this.router.navigateByUrl('/addresses');
        }
      }
    );
  }
}

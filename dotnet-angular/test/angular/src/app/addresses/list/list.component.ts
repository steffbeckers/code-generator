import { Component, OnInit } from '@angular/core';

// Models
import { Address } from 'src/app/shared/models/Address';

// Services
import { AddressService } from 'src/app/shared/services/AddressService';

@Component({
  selector: 'app-addresses-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class AddressesListComponent implements OnInit {
  public addresses: Address[];

  constructor(private addressService: AddressService) {
    this.addresses = [];
  }

  ngOnInit(): void {
    this.getAddresses();
  }

  private getAddresses(): void {
    this.addressService.getAddresses().subscribe(
      (addresses: Address[]) => {
        this.addresses = addresses;
      }
    );
  }
}

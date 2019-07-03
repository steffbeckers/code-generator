import { Component, OnInit } from '@angular/core';

// Models
import { Address } from './address';

// Services
import { AddressService } from './address.service';

@Component({
  selector: 'app-addresses',
  templateUrl: './addresses.component.html',
  styleUrls: ['./addresses.component.scss']
})
export class AddressesComponent implements OnInit {
  addresses: Address[];

  constructor(private addressService: AddressService) {}

  ngOnInit() {
    this.addressService.getAll().subscribe((addresses: Address[]) => {
      this.addresses = addresses;
    });
  }
}

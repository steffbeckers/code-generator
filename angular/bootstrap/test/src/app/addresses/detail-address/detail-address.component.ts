import { Component, OnInit } from './node_modules/@angular/core';
import { ActivatedRoute } from './node_modules/@angular/router';

// Services
import { AddressService } from '../__entityName@dasherize__.service';

// Models
import { Address } from '../__entityName@dasherize__';

@Component({
  selector: 'app-addresses-detail',
  templateUrl: './detail-address.component.html',
  styleUrls: ['./detail-address.component.scss']
})
export class DetailAddressComponent implements OnInit {
  address: Address;

  constructor(private route: ActivatedRoute, private addressService: AddressService) {}

  ngOnInit() {
    this.getAddress();
  }

  getAddress() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.addressService.getById(id).subscribe((address: Address) => {
        this.address = address;
      });
    }
  }
}

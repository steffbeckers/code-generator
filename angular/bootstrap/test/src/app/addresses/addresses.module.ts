import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { AddressService } from './address.service';

// Modules
import { AddressesRoutingModule } from './addresses-routing.module';

// Components
import { AddressesComponent } from './addresses.component';
import { CreateAddressComponent } from './create-address/create-address.component';
import { DetailAddressComponent } from './detail-address/detail-address.component';
import { EditAddressComponent } from './edit-address/edit-address.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [AddressesComponent, CreateAddressComponent, DetailAddressComponent, EditAddressComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, AddressesRoutingModule],
  providers: [AddressService]
})
export class AddressesModule {}

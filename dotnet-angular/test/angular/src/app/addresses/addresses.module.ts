import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressesRoutingModule } from './addresses-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { AddressesListComponent } from '../addresses/list/list.component';
import { AddressCreateComponent } from '../addresses/create/create.component';

@NgModule({
  declarations: [
    AddressesListComponent,
    AddressCreateComponent,
  ],
  imports: [CommonModule, AddressesRoutingModule, SharedModule]
})
export class AddressesModule {}

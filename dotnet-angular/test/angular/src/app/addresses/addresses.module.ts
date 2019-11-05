import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressesRoutingModule } from './addresses-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { AddressesListComponent } from '../addresses/list/list.component';

@NgModule({
  declarations: [
    AddressesListComponent,
  ],
  imports: [CommonModule, AddressesRoutingModule, SharedModule]
})
export class AddressesModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddressesRoutingModule } from './addresses-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { AddressesListComponent } from '../addresses/list/list.component';
import { AddressDetailComponent } from '../addresses/detail/detail.component';
import { AddressCreateComponent } from '../addresses/create/create.component';
import { AddressUpdateComponent } from '../addresses/update/update.component';

@NgModule({
  declarations: [
    AddressesListComponent,
    AddressDetailComponent,
    AddressCreateComponent,
    AddressUpdateComponent,
  ],
  imports: [CommonModule, AddressesRoutingModule, SharedModule]
})
export class AddressesModule {}

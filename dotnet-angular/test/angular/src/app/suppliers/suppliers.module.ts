import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SuppliersRoutingModule } from './suppliers-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { SuppliersListComponent } from '../suppliers/list/list.component';
import { SupplierDetailComponent } from '../suppliers/detail/detail.component';
import { SupplierCreateComponent } from '../suppliers/create/create.component';
import { SupplierUpdateComponent } from '../suppliers/update/update.component';
import { SupplierLinkComponent } from '../suppliers/link/link.component';

@NgModule({
  declarations: [
    SuppliersListComponent,
    SupplierDetailComponent,
    SupplierCreateComponent,
    SupplierUpdateComponent,
    SupplierLinkComponent,
  ],
  imports: [CommonModule, SuppliersRoutingModule, SharedModule]
})
export class SuppliersModule {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountsRoutingModule } from './accounts-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { AccountsListComponent } from '../accounts/list/list.component';
import { AccountDetailComponent } from '../accounts/detail/detail.component';
import { AccountCreateComponent } from '../accounts/create/create.component';
import { AccountUpdateComponent } from '../accounts/update/update.component';

@NgModule({
  declarations: [
    AccountsListComponent,
    AccountDetailComponent,
    AccountCreateComponent,
    AccountUpdateComponent,
  ],
  imports: [CommonModule, AccountsRoutingModule, SharedModule]
})
export class AccountsModule {}

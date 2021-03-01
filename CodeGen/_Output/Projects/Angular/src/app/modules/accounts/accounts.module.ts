import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountsComponent } from './accounts.component';
import { AccountsService } from './accounts.service';
import { AccountsListComponent } from './list/list.component';
import { AccountsDetailComponent } from './detail/detail.component';
import { AccountsCreateComponent } from './create/create.component';
import { AccountsEditComponent } from './edit/edit.component';

@NgModule({
  declarations: [
    AccountsComponent,
    AccountsListComponent,
    AccountsDetailComponent,
    AccountsCreateComponent,
    AccountsEditComponent,
  ],
  imports: [SharedModule, AccountsRoutingModule],
  providers: [AccountsService],
})
export class AccountsModule {}

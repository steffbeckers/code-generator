import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountsComponent } from './accounts.component';
import { AccountsService } from './accounts.service';
import { AccountsListComponent } from './list/list.component';

@NgModule({
  declarations: [AccountsComponent, AccountsListComponent],
  imports: [SharedModule, AccountsRoutingModule],
  providers: [AccountsService],
})
export class AccountsModule {}

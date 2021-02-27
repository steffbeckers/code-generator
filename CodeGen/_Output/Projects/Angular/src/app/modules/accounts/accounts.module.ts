import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountsComponent } from './accounts.component';
import { AccountsService } from './accounts.service';

@NgModule({
  declarations: [AccountsComponent],
  imports: [SharedModule, AccountsRoutingModule],
  providers: [AccountsService],
})
export class AccountsModule {}

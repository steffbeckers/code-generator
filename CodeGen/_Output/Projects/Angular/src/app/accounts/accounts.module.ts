import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountsComponent } from './accounts.component';

@NgModule({
  declarations: [AccountsComponent],
  imports: [SharedModule, AccountsRoutingModule],
})
export class AccountsModule {}

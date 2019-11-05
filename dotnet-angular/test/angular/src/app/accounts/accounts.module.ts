import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AccountsRoutingModule } from './accounts-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { AccountsListComponent } from '../accounts/list/list.component';

@NgModule({
  declarations: [
    AccountsListComponent,
  ],
  imports: [CommonModule, AccountsRoutingModule, SharedModule]
})
export class AccountsModule {}

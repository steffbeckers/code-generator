import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { AccountService } from './account.service';

// Modules
import { AccountsRoutingModule } from './accounts-routing.module';

// Components
import { AccountsComponent } from './accounts.component';
import { CreateAccountComponent } from './create-account/create-account.component';
import { DetailAccountComponent } from './detail-account/detail-account.component';
import { EditAccountComponent } from './edit-account/edit-account.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [AccountsComponent, CreateAccountComponent, DetailAccountComponent, EditAccountComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, AccountsRoutingModule],
  providers: [AccountService]
})
export class AccountsModule {}

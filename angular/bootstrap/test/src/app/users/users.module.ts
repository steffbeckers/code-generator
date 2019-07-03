import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { UserService } from './user.service';

// Modules
import { UsersRoutingModule } from './users-routing.module';

// Components
import { UsersComponent } from './users.component';
import { CreateUserComponent } from './create-user/create-user.component';
import { DetailUserComponent } from './detail-user/detail-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [UsersComponent, CreateUserComponent, DetailUserComponent, EditUserComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, UsersRoutingModule],
  providers: [UserService]
})
export class UsersModule {}

import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

// Modules
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

// Components
import { AppComponent } from './app.component';
import { TopNavComponent } from './shared/top-nav/top-nav.component';

// Services
import { AccountService } from './shared/services/AccountService';
import { ContactService } from './shared/services/ContactService';
import { AddressService } from './shared/services/AddressService';
import { NoteService } from './shared/services/NoteService';
import { TodoService } from './shared/services/TodoService';

@NgModule({
  declarations: [
    AppComponent,
    TopNavComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    AccountService,
    ContactService,
    AddressService,
    NoteService,
    TodoService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

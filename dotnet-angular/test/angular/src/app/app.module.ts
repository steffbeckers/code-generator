import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

// Modules
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

// Components
import { AppComponent } from './app.component';

// Services
import { AccountService } from './shared/services/AccountService';
import { ContactService } from './shared/services/ContactService';
import { AddressService } from './shared/services/AddressService';
import { NoteService } from './shared/services/NoteService';

@NgModule({
  declarations: [
    AppComponent
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
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

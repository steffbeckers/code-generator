import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

// Modules
import { AppRoutingModule } from './app-routing.module';

// Components
import { AppComponent } from './app.component';

// Services
import { AccountService } from './shared/services/AccountService';
import { AddressService } from './shared/services/AddressService';
import { ContactService } from './shared/services/ContactService';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    AccountService,
    AddressService,
    ContactService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

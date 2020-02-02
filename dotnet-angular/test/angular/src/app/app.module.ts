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
import { ProductService } from './shared/services/ProductService';
import { SupplierService } from './shared/services/SupplierService';
import { ProductDetailService } from './shared/services/ProductDetailService';

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
    ProductService,
    SupplierService,
    ProductDetailService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

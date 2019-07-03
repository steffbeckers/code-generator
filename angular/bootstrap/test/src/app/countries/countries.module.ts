import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { CountryService } from './country.service';

// Modules
import { CountriesRoutingModule } from './countries-routing.module';

// Components
import { CountriesComponent } from './countries.component';
import { CreateCountryComponent } from './create-country/create-country.component';
import { DetailCountryComponent } from './detail-country/detail-country.component';
import { EditCountryComponent } from './edit-country/edit-country.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [CountriesComponent, CreateCountryComponent, DetailCountryComponent, EditCountryComponent],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, CountriesRoutingModule],
  providers: [CountryService]
})
export class CountriesModule {}

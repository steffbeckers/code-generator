import { Component, OnInit } from '@angular/core';

// Models
import { Country } from './country';

// Services
import { CountryService } from './country.service';

@Component({
  selector: 'app-countries',
  templateUrl: './countries.component.html',
  styleUrls: ['./countries.component.scss']
})
export class CountriesComponent implements OnInit {
  countries: Country[];

  constructor(private countryService: CountryService) {}

  ngOnInit() {
    this.countryService.getAll().subscribe((countries: Country[]) => {
      this.countries = countries;
    });
  }
}

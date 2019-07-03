import { Component, OnInit } from "./node_modules/@angular/core";
import { ActivatedRoute } from "./node_modules/@angular/router";

// Services
import { CountryService } from "../__entityName@dasherize__.service";

// Models
import { Country } from "../__entityName@dasherize__";

@Component({
  selector: "app-countries-detail",
  templateUrl: "./detail-country.component.html",
  styleUrls: ["./detail-country.component.scss"]
})
export class DetailCountryComponent implements OnInit {
  country: Country;

  constructor(
    private route: ActivatedRoute,
    private countryService: CountryService
  ) {}

  ngOnInit() {
    this.getCountry();
  }

  getCountry() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.countryService.getById(id).subscribe((country: Country) => {
        this.country = country;
      });
    }
  }
}

import { Component, OnInit } from "./node_modules/@angular/core";
import { ActivatedRoute, Router } from "./node_modules/@angular/router";
import {
  FormBuilder,
  FormGroup,
  Validators,
  AbstractControl
} from "./node_modules/@angular/forms";

// RxJS
import { Observable, of } from "./node_modules/rxjs";
import {
  catchError,
  debounceTime,
  distinctUntilChanged,
  switchMap
} from "./node_modules/rxjs/operators";

// Services
import { CountryService } from "../__entityName@dasherize__.service";

// Models
import { Country } from "../__entityName@dasherize__";

// Validators
function notRequiredEmailValidator(
  control: AbstractControl
): { [key: string]: any } {
  if (!control.value) {
    return null;
  }
  return Validators.email(control);
}

@Component({
  selector: "app-countries-edit",
  templateUrl: "./edit-country.component.html",
  styleUrls: ["./edit-country.component.scss"]
})
export class EditCountryComponent implements OnInit {
  country: Country;
  countryForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private countryService: CountryService
  ) {}

  searchCountry = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.countryService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterCountry = (foundCountry: Country) => {
    return foundCountry.name;
  };

  ngOnInit() {
    this.countryForm = this.fb.group({
      id: [""],
      name: ["", [Validators.required, Validators.maxLength(50)]],
      telephone: ["", Validators.maxLength(50)],
      fax: ["", Validators.maxLength(50)],
      email: ["", [notRequiredEmailValidator, Validators.maxLength(100)]],
      website: ["", Validators.maxLength(200)],
      vatNumber: ["", Validators.maxLength(50)],
      description: ["", Validators.maxLength(4000)],
      address: this.fb.group({
        id: [""],
        street: ["", Validators.maxLength(250)],
        number: ["", Validators.maxLength(50)],
        postalcode: ["", Validators.maxLength(10)],
        city: ["", Validators.maxLength(80)],
        state: ["", Validators.maxLength(50)],
        country: "",
        latitude: [0, [Validators.min(-90), Validators.max(90)]],
        longitude: [0, [Validators.min(-180), Validators.max(180)]]
      }),
      parentCountryId: [""],
      billingCountryId: [""]
    });

    this.getCountry();
  }

  getCountry() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.countryService.getById(id).subscribe((country: Country) => {
        this.country = country;

        // Patch the country into the form
        this.countryForm.patchValue(this.country);
      });
    }
  }

  editCountry() {
    // Validate
    if (this.countryForm.invalid) {
      return;
    }

    this.countryService
      .update(this.country.id, this.countryForm.value)
      .subscribe(() => {
        this.router.navigate(["/countries", this.country.id]);
      });
  }
}

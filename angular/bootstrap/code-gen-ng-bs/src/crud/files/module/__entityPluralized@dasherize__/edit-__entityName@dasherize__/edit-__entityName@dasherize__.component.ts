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
import { <%= classify(entityName) %>Service } from "../__entityName@dasherize__.service";

// Models
import { <%= classify(entityName) %> } from "../__entityName@dasherize__";

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
  selector: "app-<%= dasherize(entityPluralized) %>-edit",
  templateUrl: "./edit-<%= dasherize(entityName) %>.component.html",
  styleUrls: ["./edit-<%= dasherize(entityName) %>.component.scss"]
})
export class Edit<%= classify(entityName) %>Component implements OnInit {
  <%= dasherize(entityName) %>: <%= classify(entityName) %>;
  <%= dasherize(entityName) %>Form: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private <%= dasherize(entityName) %>Service: <%= classify(entityName) %>Service
  ) {}

  search<%= classify(entityName) %> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.<%= dasherize(entityName) %>Service.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatter<%= classify(entityName) %> = (found<%= classify(entityName) %>: <%= classify(entityName) %>) => {
    return found<%= classify(entityName) %>.name;
  };

  ngOnInit() {
    this.<%= dasherize(entityName) %>Form = this.fb.group({
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
      parent<%= classify(entityName) %>Id: [""],
      billing<%= classify(entityName) %>Id: [""]
    });

    this.get<%= classify(entityName) %>();
  }

  get<%= classify(entityName) %>() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.<%= dasherize(entityName) %>Service.getById(id).subscribe((<%= dasherize(entityName) %>: <%= classify(entityName) %>) => {
        this.<%= dasherize(entityName) %> = <%= dasherize(entityName) %>;

        // Patch the <%= dasherize(entityName) %> into the form
        this.<%= dasherize(entityName) %>Form.patchValue(this.<%= dasherize(entityName) %>);
      });
    }
  }

  edit<%= classify(entityName) %>() {
    // Validate
    if (this.<%= dasherize(entityName) %>Form.invalid) {
      return;
    }

    this.<%= dasherize(entityName) %>Service
      .update(this.<%= dasherize(entityName) %>.id, this.<%= dasherize(entityName) %>Form.value)
      .subscribe(() => {
        this.router.navigate(["/<%= dasherize(entityPluralized) %>", this.<%= dasherize(entityName) %>.id]);
      });
  }
}

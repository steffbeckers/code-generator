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
import { AccountRelationTypeService } from "../__entityName@dasherize__.service";

// Models
import { AccountRelationType } from "../__entityName@dasherize__";

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
  selector: "app-account-relation-types-edit",
  templateUrl: "./edit-account-relation-type.component.html",
  styleUrls: ["./edit-account-relation-type.component.scss"]
})
export class EditAccountRelationTypeComponent implements OnInit {
  account-relation-type: AccountRelationType;
  account-relation-typeForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private account-relation-typeService: AccountRelationTypeService
  ) {}

  searchAccountRelationType = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.account-relation-typeService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterAccountRelationType = (foundAccountRelationType: AccountRelationType) => {
    return foundAccountRelationType.name;
  };

  ngOnInit() {
    this.account-relation-typeForm = this.fb.group({
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
      parentAccountRelationTypeId: [""],
      billingAccountRelationTypeId: [""]
    });

    this.getAccountRelationType();
  }

  getAccountRelationType() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.account-relation-typeService.getById(id).subscribe((account-relation-type: AccountRelationType) => {
        this.account-relation-type = account-relation-type;

        // Patch the account-relation-type into the form
        this.account-relation-typeForm.patchValue(this.account-relation-type);
      });
    }
  }

  editAccountRelationType() {
    // Validate
    if (this.account-relation-typeForm.invalid) {
      return;
    }

    this.account-relation-typeService
      .update(this.account-relation-type.id, this.account-relation-typeForm.value)
      .subscribe(() => {
        this.router.navigate(["/account-relation-types", this.account-relation-type.id]);
      });
  }
}

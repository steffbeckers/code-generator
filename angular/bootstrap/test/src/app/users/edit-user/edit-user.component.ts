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
import { UserService } from "../__entityName@dasherize__.service";

// Models
import { User } from "../__entityName@dasherize__";

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
  selector: "app-users-edit",
  templateUrl: "./edit-user.component.html",
  styleUrls: ["./edit-user.component.scss"]
})
export class EditUserComponent implements OnInit {
  user: User;
  userForm: FormGroup;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private userService: UserService
  ) {}

  searchUser = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(term =>
        this.userService.getByName(term).pipe(
          catchError(() => {
            return of([]);
          })
        )
      )
    );
  inputFormatterUser = (foundUser: User) => {
    return foundUser.name;
  };

  ngOnInit() {
    this.userForm = this.fb.group({
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
      parentUserId: [""],
      billingUserId: [""]
    });

    this.getUser();
  }

  getUser() {
    let id = this.route.snapshot.params && this.route.snapshot.params.id;
    if (id) {
      this.userService.getById(id).subscribe((user: User) => {
        this.user = user;

        // Patch the user into the form
        this.userForm.patchValue(this.user);
      });
    }
  }

  editUser() {
    // Validate
    if (this.userForm.invalid) {
      return;
    }

    this.userService
      .update(this.user.id, this.userForm.value)
      .subscribe(() => {
        this.router.navigate(["/users", this.user.id]);
      });
  }
}

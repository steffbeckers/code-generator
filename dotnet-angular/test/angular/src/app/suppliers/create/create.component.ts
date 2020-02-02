import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

// Services
import { SupplierService } from 'src/app/shared/services/SupplierService';

@Component({
  selector: 'app-supplier-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class SupplierCreateComponent implements OnInit {
  // Supplier
  public supplierForm: FormGroup;
  public creating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private supplierService: SupplierService
  ) {
    this.creating = false;
  }

  ngOnInit(): void {
    this.supplierForm = this.fb.group({
      name: ['', Validators.required],
      phone: [''],
      productId: [''],
    });

    // Patch query params to form from URL
    this.route.queryParams.subscribe((queryParams) => {
      this.supplierForm.patchValue(queryParams);
    });
  }

  public createSupplier(): void {
    // Validate
    if (this.supplierForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.supplierService.createSupplier(this.supplierForm.value).subscribe(
      (supplier: Supplier) => {
        this.creating = false;

        this.router.navigateByUrl('/suppliers/' + supplier.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

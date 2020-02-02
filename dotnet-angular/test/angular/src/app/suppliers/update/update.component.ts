import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

// Services
import { SupplierService } from 'src/app/shared/services/SupplierService';

@Component({
  selector: 'app-supplier-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class SupplierUpdateComponent implements OnInit {
  // Supplier
  public supplier: Supplier;
  public supplierForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private supplierService: SupplierService
  ) {
    this.supplier = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.supplierForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      phone: [''],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSupplier(routeParams.id);
    });
  }

  private getSupplier(id: string): void {
    this.supplierService.getSupplier(id).subscribe(
      (supplier: Supplier) => {
        this.supplier = supplier;
        this.supplierForm.patchValue(this.supplier);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Supplier could not be found.');
          this.router.navigateByUrl('/suppliers');
        }
      }
    );
  }

  public updateSupplier(andClose: boolean = false): void {
    // Validate
    if (this.supplierForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.supplierForm.pristine && andClose) {
      this.router.navigateByUrl('/suppliers/' + this.supplier.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.supplierService.updateSupplier(this.supplierForm.value).subscribe(
      (supplier: Supplier) => {
        if (andClose) {
          this.router.navigateByUrl('/suppliers/' + supplier.id);
        }

        this.supplier = supplier;
        this.supplierForm.patchValue(this.supplier);
      },
      null,
      () => {
        this.updating = false;
      }
    );
  }

  public deleteSupplier(): void {
    // Validate
    if (!this.supplier && !this.supplier.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete supplier: ' + this.supplier.id + '?')) {
      this.supplierService.deleteSupplier(this.supplier.id).subscribe(
        () => {
          this.router.navigateByUrl('/suppliers');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Supplier could not be found.');
            this.router.navigateByUrl('/suppliers');
          }
        }
      );
    }
  }
}

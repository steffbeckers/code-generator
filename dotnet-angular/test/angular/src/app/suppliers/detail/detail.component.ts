import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

// Services
import { SupplierService } from 'src/app/shared/services/SupplierService';

@Component({
  selector: 'app-supplier-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class SupplierDetailComponent implements OnInit {
  // Supplier
  public supplier: Supplier;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private supplierService: SupplierService
  ) {
    this.supplier = null;
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getSupplier(routeParams.id);
    });
  }

  private getSupplier(id: string): void {
    this.supplierService.getSupplier(id).subscribe(
      (supplier: Supplier) => {
        this.supplier = supplier;
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Supplier could not be found.');
          this.router.navigateByUrl('/suppliers');
        }
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

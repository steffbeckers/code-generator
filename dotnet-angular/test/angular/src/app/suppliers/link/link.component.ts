import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

// Services
import { SupplierService } from 'src/app/shared/services/SupplierService';

@Component({
  selector: 'app-supplier-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class SupplierLinkComponent implements OnInit {
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
}

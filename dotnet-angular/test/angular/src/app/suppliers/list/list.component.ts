import { Component, OnInit } from '@angular/core';

// Models
import { Supplier } from 'src/app/shared/models/Supplier';

// Services
import { SupplierService } from 'src/app/shared/services/SupplierService';

@Component({
  selector: 'app-suppliers-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class SuppliersListComponent implements OnInit {
  public suppliers: Supplier[];

  constructor(private supplierService: SupplierService) {
    this.suppliers = null;
  }

  ngOnInit(): void {
    this.getSuppliers();
  }

  private getSuppliers(): void {
    this.supplierService.getSuppliers().subscribe(
      (suppliers: Supplier[]) => {
        this.suppliers = suppliers;
      }
    );
  }
}

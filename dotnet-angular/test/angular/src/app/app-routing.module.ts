import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'products',
    loadChildren: './products/products.module#ProductsModule',
  },
  {
    path: 'suppliers',
    loadChildren: './suppliers/suppliers.module#SuppliersModule',
  },
  {
    path: 'productdetails',
    loadChildren: './productdetails/productdetails.module#ProductDetailsModule',
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

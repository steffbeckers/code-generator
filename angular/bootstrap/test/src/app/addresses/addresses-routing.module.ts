import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { AddressesComponent } from "./__entityPluralized@dasherize__.component";
import { CreateAddressComponent } from "./create-address/create-address.component";
import { DetailAddressComponent } from "./detail-address/detail-address.component";
import { EditAddressComponent } from "./edit-address/edit-address.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "addresses/create",
      component: CreateAddressComponent
    },
    {
      path: "addresses/:id/edit",
      component: EditAddressComponent
    },
    {
      path: "addresses/:id",
      component: DetailAddressComponent
    },
    {
      path: "addresses",
      component: AddressesComponent,
      data: { title: extract("Addresses") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AddressesRoutingModule {}

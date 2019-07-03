import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { CountriesComponent } from "./__entityPluralized@dasherize__.component";
import { CreateCountryComponent } from "./create-country/create-country.component";
import { DetailCountryComponent } from "./detail-country/detail-country.component";
import { EditCountryComponent } from "./edit-country/edit-country.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "countries/create",
      component: CreateCountryComponent
    },
    {
      path: "countries/:id/edit",
      component: EditCountryComponent
    },
    {
      path: "countries/:id",
      component: DetailCountryComponent
    },
    {
      path: "countries",
      component: CountriesComponent,
      data: { title: extract("Countries") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class CountriesRoutingModule {}

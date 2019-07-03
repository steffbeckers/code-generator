import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { AccountRelationTypesComponent } from "./__entityPluralized@dasherize__.component";
import { CreateAccountRelationTypeComponent } from "./create-account-relation-type/create-account-relation-type.component";
import { DetailAccountRelationTypeComponent } from "./detail-account-relation-type/detail-account-relation-type.component";
import { EditAccountRelationTypeComponent } from "./edit-account-relation-type/edit-account-relation-type.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "account-relation-types/create",
      component: CreateAccountRelationTypeComponent
    },
    {
      path: "account-relation-types/:id/edit",
      component: EditAccountRelationTypeComponent
    },
    {
      path: "account-relation-types/:id",
      component: DetailAccountRelationTypeComponent
    },
    {
      path: "account-relation-types",
      component: AccountRelationTypesComponent,
      data: { title: extract("AccountRelationTypes") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountRelationTypesRoutingModule {}

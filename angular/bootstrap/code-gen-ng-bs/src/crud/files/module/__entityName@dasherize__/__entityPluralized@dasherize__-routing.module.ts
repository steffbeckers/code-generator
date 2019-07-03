import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { AccountsComponent } from "./__entityPluralized@dasherize__.component";
import { CreateAccountComponent } from "./create-account/create-account.component";
import { DetailAccountComponent } from "./detail-account/detail-account.component";
import { EditAccountComponent } from "./edit-account/edit-account.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "accounts/create",
      component: CreateAccountComponent
    },
    {
      path: "accounts/:id/edit",
      component: EditAccountComponent
    },
    {
      path: "accounts/:id",
      component: DetailAccountComponent
    },
    {
      path: "accounts",
      component: AccountsComponent,
      data: { title: extract("Accounts") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountsRoutingModule {}

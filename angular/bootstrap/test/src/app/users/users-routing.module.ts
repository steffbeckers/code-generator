import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { UsersComponent } from "./__entityPluralized@dasherize__.component";
import { CreateUserComponent } from "./create-user/create-user.component";
import { DetailUserComponent } from "./detail-user/detail-user.component";
import { EditUserComponent } from "./edit-user/edit-user.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "users/create",
      component: CreateUserComponent
    },
    {
      path: "users/:id/edit",
      component: EditUserComponent
    },
    {
      path: "users/:id",
      component: DetailUserComponent
    },
    {
      path: "users",
      component: UsersComponent,
      data: { title: extract("Users") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class UsersRoutingModule {}

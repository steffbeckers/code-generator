import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { <%= classify(entityPluralized) %>Component } from "./__entityPluralized@dasherize__.component";
import { Create<%= classify(entityName) %>Component } from "./create-<%= dasherize(entityName) %>/create-<%= dasherize(entityName) %>.component";
import { Detail<%= classify(entityName) %>Component } from "./detail-<%= dasherize(entityName) %>/detail-<%= dasherize(entityName) %>.component";
import { Edit<%= classify(entityName) %>Component } from "./edit-<%= dasherize(entityName) %>/edit-<%= dasherize(entityName) %>.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "<%= dasherize(entityPluralized) %>/create",
      component: Create<%= classify(entityName) %>Component
    },
    {
      path: "<%= dasherize(entityPluralized) %>/:id/edit",
      component: Edit<%= classify(entityName) %>Component
    },
    {
      path: "<%= dasherize(entityPluralized) %>/:id",
      component: Detail<%= classify(entityName) %>Component
    },
    {
      path: "<%= dasherize(entityPluralized) %>",
      component: <%= classify(entityPluralized) %>Component,
      data: { title: extract("<%= classify(entityPluralized) %>") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class <%= classify(entityPluralized) %>RoutingModule {}

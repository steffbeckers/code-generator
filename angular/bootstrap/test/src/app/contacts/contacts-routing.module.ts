import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { extract } from "@app/core";
import { Shell } from "@app/shell/shell.service";

import { ContactsComponent } from "./__entityPluralized@dasherize__.component";
import { CreateContactComponent } from "./create-contact/create-contact.component";
import { DetailContactComponent } from "./detail-contact/detail-contact.component";
import { EditContactComponent } from "./edit-contact/edit-contact.component";

const routes: Routes = [
  Shell.childRoutes([
    {
      path: "contacts/create",
      component: CreateContactComponent
    },
    {
      path: "contacts/:id/edit",
      component: EditContactComponent
    },
    {
      path: "contacts/:id",
      component: DetailContactComponent
    },
    {
      path: "contacts",
      component: ContactsComponent,
      data: { title: extract("Contacts") }
    }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class ContactsRoutingModule {}

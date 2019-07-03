import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

// Services
import { <%= classify(entityName) %>Service } from './<%= dasherize(entityName) %>.service';

// Modules
import { <%= classify(entityPluralized) %>RoutingModule } from './<%= dasherize(entityPluralized) %>-routing.module';

// Components
import { <%= classify(entityPluralized) %>Component } from './<%= dasherize(entityPluralized) %>.component';
import { Create<%= classify(entityName) %>Component } from './create-<%= dasherize(entityName) %>/create-<%= dasherize(entityName) %>.component';
import { Detail<%= classify(entityName) %>Component } from './detail-<%= dasherize(entityName) %>/detail-<%= dasherize(entityName) %>.component';
import { Edit<%= classify(entityName) %>Component } from './edit-<%= dasherize(entityName) %>/edit-<%= dasherize(entityName) %>.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [<%= classify(entityPluralized) %>Component, Create<%= classify(entityName) %>Component, Detail<%= classify(entityName) %>Component, Edit<%= classify(entityName) %>Component],
  imports: [CommonModule, ReactiveFormsModule, NgbModule, <%= classify(entityPluralized) %>RoutingModule],
  providers: [<%= classify(entityName) %>Service]
})
export class <%= classify(entityPluralized) %>Module {}

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodosRoutingModule } from './todos-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { TodosListComponent } from '../todos/list/list.component';
import { TodoDetailComponent } from '../todos/detail/detail.component';
import { TodoCreateComponent } from '../todos/create/create.component';

@NgModule({
  declarations: [
    TodosListComponent,
    TodoDetailComponent,
    TodoCreateComponent,
  ],
  imports: [CommonModule, TodosRoutingModule, SharedModule]
})
export class TodosModule {}

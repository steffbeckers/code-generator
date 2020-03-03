import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

// Components
import { TodosListComponent } from './list/list.component';
import { TodoDetailComponent } from './detail/detail.component';
import { TodoCreateComponent } from './create/create.component';
import { TodoUpdateComponent } from './update/update.component';
import { TodoLinkComponent } from './link/link.component';

const routes: Routes = [
  {
    path: 'create',
    component: TodoCreateComponent
  },
  {
    path: ':id/link/:model',
    component: TodoLinkComponent
  },
  {
    path: ':id/edit',
    component: TodoUpdateComponent
  },
  {
    path: ':id',
    component: TodoDetailComponent
  },
  {
    path: '',
    component: TodosListComponent
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodosRoutingModule {}

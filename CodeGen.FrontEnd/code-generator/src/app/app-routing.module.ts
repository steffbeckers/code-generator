import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'projects',
    loadChildren: () =>
      import('./projects/projects.module').then((m) => m.ProjectsModule),
  },
  {
    path: '**',
    pathMatch: 'full',
    redirectTo: 'projects',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

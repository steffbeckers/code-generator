import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'documents',
    loadChildren: './documents/documents.module#DocumentsModule',
  },
  {
    path: 'resumes',
    loadChildren: './resumes/resumes.module#ResumesModule',
  },
  {
    path: 'resumestates',
    loadChildren: './resumestates/resumestates.module#ResumeStatesModule',
  },
  {
    path: 'skills',
    loadChildren: './skills/skills.module#SkillsModule',
  },
  {
    path: 'skillaliases',
    loadChildren: './skillaliases/skillaliases.module#SkillAliasesModule',
  },
  {
    path: 'jobs',
    loadChildren: './jobs/jobs.module#JobsModule',
  },
  {
    path: 'jobstates',
    loadChildren: './jobstates/jobstates.module#JobStatesModule',
  },
  {
    path: '**',
    redirectTo: '',
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

// Modules
import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';

// Components
import { AppComponent } from './app.component';
import { TopNavComponent } from './shared/top-nav/top-nav.component';

// Services
import { DocumentService } from './shared/services/DocumentService';
import { ResumeService } from './shared/services/ResumeService';
import { ResumeStateService } from './shared/services/ResumeStateService';
import { SkillService } from './shared/services/SkillService';
import { SkillAliasService } from './shared/services/SkillAliasService';
import { JobService } from './shared/services/JobService';
import { JobStateService } from './shared/services/JobStateService';

@NgModule({
  declarations: [
    AppComponent,
    TopNavComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    DocumentService,
    ResumeService,
    ResumeStateService,
    SkillService,
    SkillAliasService,
    JobService,
    JobStateService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

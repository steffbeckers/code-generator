import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DocumentsRoutingModule } from './documents-routing.module';
import { SharedModule } from '../shared/shared.module';

// Components
import { DocumentsListComponent } from '../documents/list/list.component';
import { DocumentDetailComponent } from '../documents/detail/detail.component';
import { DocumentCreateComponent } from '../documents/create/create.component';
import { DocumentUpdateComponent } from '../documents/update/update.component';
import { DocumentLinkComponent } from '../documents/link/link.component';

@NgModule({
  declarations: [
    DocumentsListComponent,
    DocumentDetailComponent,
    DocumentCreateComponent,
    DocumentUpdateComponent,
    DocumentLinkComponent,
  ],
  imports: [CommonModule, DocumentsRoutingModule, SharedModule]
})
export class DocumentsModule {}

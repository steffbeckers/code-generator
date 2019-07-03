import { Component, OnInit } from '@angular/core';

// Models
import { AccountRelationType } from './account-relation-type';

// Services
import { AccountRelationTypeService } from './account-relation-type.service';

@Component({
  selector: 'app-account-relation-types',
  templateUrl: './account-relation-types.component.html',
  styleUrls: ['./account-relation-types.component.scss']
})
export class AccountRelationTypesComponent implements OnInit {
  account-relation-types: AccountRelationType[];

  constructor(private account-relation-typeService: AccountRelationTypeService) {}

  ngOnInit() {
    this.account-relation-typeService.getAll().subscribe((account-relation-types: AccountRelationType[]) => {
      this.account-relation-types = account-relation-types;
    });
  }
}

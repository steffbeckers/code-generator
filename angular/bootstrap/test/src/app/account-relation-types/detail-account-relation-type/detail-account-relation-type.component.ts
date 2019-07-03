import { Component, OnInit } from "./node_modules/@angular/core";
import { ActivatedRoute } from "./node_modules/@angular/router";

// Services
import { AccountRelationTypeService } from "../__entityName@dasherize__.service";

// Models
import { AccountRelationType } from "../__entityName@dasherize__";

@Component({
  selector: "app-account-relation-types-detail",
  templateUrl: "./detail-account-relation-type.component.html",
  styleUrls: ["./detail-account-relation-type.component.scss"]
})
export class DetailAccountRelationTypeComponent implements OnInit {
  account-relation-type: AccountRelationType;

  constructor(
    private route: ActivatedRoute,
    private account-relation-typeService: AccountRelationTypeService
  ) {}

  ngOnInit() {
    this.getAccountRelationType();
  }

  getAccountRelationType() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.account-relation-typeService.getById(id).subscribe((account-relation-type: AccountRelationType) => {
        this.account-relation-type = account-relation-type;
      });
    }
  }
}

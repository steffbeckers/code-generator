import { Component, OnInit } from "./node_modules/@angular/core";
import { ActivatedRoute } from "./node_modules/@angular/router";

// Services
import { <%= classify(entityName) %>Service } from "../__entityName@dasherize__.service";

// Models
import { <%= classify(entityName) %> } from "../__entityName@dasherize__";

@Component({
  selector: "app-<%= dasherize(entityPluralized) %>-detail",
  templateUrl: "./detail-<%= dasherize(entityName) %>.component.html",
  styleUrls: ["./detail-<%= dasherize(entityName) %>.component.scss"]
})
export class Detail<%= classify(entityName) %>Component implements OnInit {
  <%= dasherize(entityName) %>: <%= classify(entityName) %>;

  constructor(
    private route: ActivatedRoute,
    private <%= dasherize(entityName) %>Service: <%= classify(entityName) %>Service
  ) {}

  ngOnInit() {
    this.get<%= classify(entityName) %>();
  }

  get<%= classify(entityName) %>() {
    let id = this.route.snapshot.params.id;
    if (id) {
      this.<%= dasherize(entityName) %>Service.getById(id).subscribe((<%= dasherize(entityName) %>: <%= classify(entityName) %>) => {
        this.<%= dasherize(entityName) %> = <%= dasherize(entityName) %>;
      });
    }
  }
}

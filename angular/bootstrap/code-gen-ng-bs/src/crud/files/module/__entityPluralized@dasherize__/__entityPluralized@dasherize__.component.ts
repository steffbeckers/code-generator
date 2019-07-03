import { Component, OnInit } from '@angular/core';

// Models
import { <%= classify(entityName) %> } from './<%= dasherize(entityName) %>';

// Services
import { <%= classify(entityName) %>Service } from './<%= dasherize(entityName) %>.service';

@Component({
  selector: 'app-<%= dasherize(entityPluralized) %>',
  templateUrl: './<%= dasherize(entityPluralized) %>.component.html',
  styleUrls: ['./<%= dasherize(entityPluralized) %>.component.scss']
})
export class <%= classify(entityPluralized) %>Component implements OnInit {
  <%= dasherize(entityPluralized) %>: <%= classify(entityName) %>[];

  constructor(private <%= dasherize(entityName) %>Service: <%= classify(entityName) %>Service) {}

  ngOnInit() {
    this.<%= dasherize(entityName) %>Service.getAll().subscribe((<%= dasherize(entityPluralized) %>: <%= classify(entityName) %>[]) => {
      this.<%= dasherize(entityPluralized) %> = <%= dasherize(entityPluralized) %>;
    });
  }
}

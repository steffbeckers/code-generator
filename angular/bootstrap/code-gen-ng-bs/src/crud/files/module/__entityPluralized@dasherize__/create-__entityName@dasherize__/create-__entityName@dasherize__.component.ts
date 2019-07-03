import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-<%= dasherize(entityPluralized) %>-create',
  templateUrl: './create-<%= dasherize(entityName) %>.component.html',
  styleUrls: ['./create-<%= dasherize(entityName) %>.component.scss']
})
export class Create<%= classify(entityName) %>Component implements OnInit {
  constructor() {}

  ngOnInit() {}
}

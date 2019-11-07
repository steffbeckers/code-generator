import { Component, OnInit } from '@angular/core';

// Models
import { Todo } from 'src/app/shared/models/Todo';

// Services
import { TodoService } from 'src/app/shared/services/TodoService';

@Component({
  selector: 'app-todos-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class TodosListComponent implements OnInit {
  public todos: Todo[];

  constructor(private todoService: TodoService) {
    this.todos = null;
  }

  ngOnInit(): void {
    this.getTodos();
  }

  private getTodos(): void {
    this.todoService.getTodos().subscribe(
      (todos: Todo[]) => {
        this.todos = todos;
      }
    );
  }
}

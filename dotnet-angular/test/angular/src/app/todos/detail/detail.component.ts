import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Todo } from 'src/app/shared/models/Todo';

// Services
import { TodoService } from 'src/app/shared/services/TodoService';

@Component({
  selector: 'app-todo-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss']
})
export class TodoDetailComponent implements OnInit {
  // Todo
  public todo: Todo;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private todoService: TodoService
  ) {
    this.todo = new Todo();
  }

  ngOnInit(): void {
    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getTodo(routeParams.id);
    });
  }

  private getTodo(id: string): void {
    this.todoService.getTodo(id).subscribe(
      (todo: Todo) => {
        this.todo = todo;
      }
    );
  }

  public deleteTodo(): void {
    // Validate
    if (!this.todo && !this.todo.id) {
      return;
    }

    // Confirmation
    // #-#-# {C7F36FD4-5D57-4CBB-8B49-D6781BD5E2D0}
    if (confirm('Are you sure you want to delete todo: ' + this.todo.title + '?')) {
    // #-#-#
      this.todoService.deleteTodo(this.todo.id).subscribe(
        () => {
          this.router.navigateByUrl('/todos');
        }
      );
    }
  }
}

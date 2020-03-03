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
    this.todo = null;
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
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Todo could not be found.');
          this.router.navigateByUrl('/todos');
        }
      }
    );
  }

  public deleteTodo(): void {
    // Validate
    if (!this.todo && !this.todo.id) {
      return;
    }

    // Confirmation
    if (confirm('Are you sure you want to delete todo: ' + this.todo.title + '?')) {
      this.todoService.deleteTodo(this.todo.id).subscribe(
        () => {
          this.router.navigateByUrl('/todos');
        },
        (error: any) => {
          if (error.status === 404) {
            alert('Todo could not be found.');
            this.router.navigateByUrl('/todos');
          }
        }
      );
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

// Models
import { Todo } from 'src/app/shared/models/Todo';

// Services
import { TodoService } from 'src/app/shared/services/TodoService';

@Component({
  selector: 'app-todo-link',
  templateUrl: './link.component.html',
  styleUrls: ['./link.component.scss']
})
export class TodoLinkComponent implements OnInit {
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
}

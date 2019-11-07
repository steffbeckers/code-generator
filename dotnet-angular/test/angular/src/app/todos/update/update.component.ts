import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Todo } from 'src/app/shared/models/Todo';

// Services
import { TodoService } from 'src/app/shared/services/TodoService';

@Component({
  selector: 'app-todo-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.scss']
})
export class TodoUpdateComponent implements OnInit {
  // Todo
  public todo: Todo;
  public todoForm: FormGroup;
  public updating: boolean;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private todoService: TodoService
  ) {
    this.todo = null;
    this.updating = false;
  }

  ngOnInit(): void {
    this.todoForm = this.fb.group({
      id: ['', Validators.required],
      title: ['', Validators.required],
      dueDate: [''],
    });

    // Get id from params
    this.route.params.subscribe((routeParams) => {
      this.getTodo(routeParams.id);
    });
  }

  private getTodo(id: string): void {
    this.todoService.getTodo(id).subscribe(
      (todo: Todo) => {
        this.todo = todo;
        this.todoForm.patchValue(this.todo);
      },
      (error: any) => {
        if (error.status === 404) {
          alert('Todo could not be found.');
          this.router.navigateByUrl('/todos');
        }
      }
    );
  }

  public updateTodo(andClose: boolean = false): void {
    // Validate
    if (this.todoForm.invalid || this.updating) {
      return;
    }

    // Only close when nothing changed
    if (this.todoForm.pristine && andClose) {
      this.router.navigateByUrl('/todos/' + this.todo.id);
      return;
    }

    // Already updating check
    this.updating = true;

    this.todoService.updateTodo(this.todoForm.value).subscribe(
      (todo: Todo) => {
        if (andClose) {
          this.router.navigateByUrl('/todos/' + todo.id);
        }

        this.todo = todo;
        this.todoForm.patchValue(this.todo);
      },
      null,
      () => {
        this.updating = false;
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

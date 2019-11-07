import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

// Models
import { Todo } from 'src/app/shared/models/Todo';

// Services
import { TodoService } from 'src/app/shared/services/TodoService';

@Component({
  selector: 'app-todo-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class TodoCreateComponent implements OnInit {
  // Todo
  public todoForm: FormGroup;
  public creating = false;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private todoService: TodoService
  ) {}

  ngOnInit(): void {
    this.todoForm = this.fb.group({
      title: ['', Validators.required],
      dueDate: [''],
    });
  }

  public createTodo(): void {
    // Validate
    if (this.todoForm.invalid || this.creating) {
      return;
    }

    // Already creating check
    this.creating = true;

    this.todoService.createTodo(this.todoForm.value).subscribe(
      (todo: Todo) => {
        this.creating = false;

        this.router.navigateByUrl('/todos/' + todo.id);
      },
      null,
      () => {
        this.creating = false;
      }
    );
  }
}

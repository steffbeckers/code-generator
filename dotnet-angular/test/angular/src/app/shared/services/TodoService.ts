import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Todo } from 'src/app/shared/models/Todo';

@Injectable({ providedIn: 'root' })
export class TodoService {
  constructor(private http: HttpClient) {}

  // GET: api/todos
  // Retrieves all todos.
  public getTodos(): Observable<Todo[]> {
    return this.http.get<Todo[]>(`${environment.api}/todos`);
  }

  // GET: api/todos/{id}
  // Retrieves a specific todo.
  public getTodo(todo: Todo | string): Observable<Todo> {
    const id = typeof todo === 'string' ? todo : (todo as Todo).id;
    return this.http.get<Todo>(`${environment.api}/todos/${id}`);
  }

  // POST: api/todos
  // Creates a new todo.
  public createTodo(todo: Todo): Observable<Todo> {
    return this.http.post<Todo>(`${environment.api}/todos`, todo);
  }

  // PUT: api/todos/{id}
  // Updates a specific todo.
  public updateTodo(todo: Todo): Observable<Todo> {
    return this.http.put<Todo>(`${environment.api}/todos/${todo.id}`, todo);
  }

  // DELETE: api/todos/{id}
  // Deletes a specific todo.
  public deleteTodo(todo: Todo | string): Observable<Todo> {
    const id = typeof todo === 'string' ? todo : (todo as Todo).id;
    return this.http.delete<Todo>(`${environment.api}/todos/${id}`);
  }
}

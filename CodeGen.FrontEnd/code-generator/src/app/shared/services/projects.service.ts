import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/models/response.model';
import { environment } from 'src/environments/environment';

@Injectable()
export class ProjectsService {
  constructor(private http: HttpClient) {}

  getProjects(): Observable<Response> {
    return this.http.get<Response>(`${environment.api}/projects`);
  }

  getProjectById(id: string) {
    return this.http.get<Response>(`${environment.api}/projects/${id}`);
  }

  generateProjectById(id: string) {
    return this.http.get<Response>(
      `${environment.api}/projects/${id}/generate`
    );
  }
}

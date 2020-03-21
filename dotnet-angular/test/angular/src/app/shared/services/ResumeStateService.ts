import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { ResumeState } from 'src/app/shared/models/ResumeState';

@Injectable({ providedIn: 'root' })
export class ResumeStateService {
  constructor(private http: HttpClient) {}

  // GET: api/resumestates
  // Retrieves all resumestates.
  public getResumeStates(): Observable<ResumeState[]> {
    return this.http.get<ResumeState[]>(`${environment.api}/resumestates`);
  }

  // GET: api/resumestates/{id}
  // Retrieves a specific resumestate.
  public getResumeState(resumeState: ResumeState | string): Observable<ResumeState> {
    const id = typeof resumeState === 'string' ? resumeState : (resumeState as ResumeState).id;
    return this.http.get<ResumeState>(`${environment.api}/resumestates/${id}`);
  }

  // POST: api/resumestates
  // Creates a new resumestate.
  public createResumeState(resumeState: ResumeState): Observable<ResumeState> {
    return this.http.post<ResumeState>(`${environment.api}/resumestates`, resumeState);
  }

  // PUT: api/resumestates/{id}
  // Updates a specific resumestate.
  public updateResumeState(resumeState: ResumeState): Observable<ResumeState> {
    return this.http.put<ResumeState>(`${environment.api}/resumestates/${resumeState.id}`, resumeState);
  }

  // DELETE: api/resumestates/{id}
  // Deletes a specific resumestate.
  public deleteResumeState(resumeState: ResumeState | string): Observable<ResumeState> {
    const id = typeof resumeState === 'string' ? resumeState : (resumeState as ResumeState).id;
    return this.http.delete<ResumeState>(`${environment.api}/resumestates/${id}`);
  }
}

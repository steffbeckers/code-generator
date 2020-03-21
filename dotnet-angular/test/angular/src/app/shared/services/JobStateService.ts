import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { JobState } from 'src/app/shared/models/JobState';

@Injectable({ providedIn: 'root' })
export class JobStateService {
  constructor(private http: HttpClient) {}

  // GET: api/jobstates
  // Retrieves all jobstates.
  public getJobStates(): Observable<JobState[]> {
    return this.http.get<JobState[]>(`${environment.api}/jobstates`);
  }

  // GET: api/jobstates/{id}
  // Retrieves a specific jobstate.
  public getJobState(jobState: JobState | string): Observable<JobState> {
    const id = typeof jobState === 'string' ? jobState : (jobState as JobState).id;
    return this.http.get<JobState>(`${environment.api}/jobstates/${id}`);
  }

  // POST: api/jobstates
  // Creates a new jobstate.
  public createJobState(jobState: JobState): Observable<JobState> {
    return this.http.post<JobState>(`${environment.api}/jobstates`, jobState);
  }

  // PUT: api/jobstates/{id}
  // Updates a specific jobstate.
  public updateJobState(jobState: JobState): Observable<JobState> {
    return this.http.put<JobState>(`${environment.api}/jobstates/${jobState.id}`, jobState);
  }

  // DELETE: api/jobstates/{id}
  // Deletes a specific jobstate.
  public deleteJobState(jobState: JobState | string): Observable<JobState> {
    const id = typeof jobState === 'string' ? jobState : (jobState as JobState).id;
    return this.http.delete<JobState>(`${environment.api}/jobstates/${id}`);
  }
}

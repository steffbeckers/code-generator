import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Job } from 'src/app/shared/models/Job';

@Injectable({ providedIn: 'root' })
export class JobService {
  constructor(private http: HttpClient) {}

  // GET: api/jobs
  // Retrieves all jobs.
  public getJobs(): Observable<Job[]> {
    return this.http.get<Job[]>(`${environment.api}/jobs`);
  }

  // GET: api/jobs/{id}
  // Retrieves a specific job.
  public getJob(job: Job | string): Observable<Job> {
    const id = typeof job === 'string' ? job : (job as Job).id;
    return this.http.get<Job>(`${environment.api}/jobs/${id}`);
  }

  // POST: api/jobs
  // Creates a new job.
  public createJob(job: Job): Observable<Job> {
    return this.http.post<Job>(`${environment.api}/jobs`, job);
  }

  // PUT: api/jobs/{id}
  // Updates a specific job.
  public updateJob(job: Job): Observable<Job> {
    return this.http.put<Job>(`${environment.api}/jobs/${job.id}`, job);
  }

  // PUT: api/Jobs/{jobId}/skills/{skillId}/link
  // Links a specific skill to job.
  public linkSkillToJob(jobId: string, skillId: string): Observable<Job> {
    return this.http.put<Job>(`${environment.api}/jobs/${jobId}/skills/${skillId}/link`, null);
  }

  // PUT: api/Jobs/{jobId}/skills/{skillId}/unlink
  // Unlinks a specific skill from job.
  public unlinkSkillFromJob(jobId: string, skillId: string): Observable<Job> {
    return this.http.put<Job>(`${environment.api}/jobs/${jobId}/skills/${skillId}/unlink`, null);
  }

  // DELETE: api/jobs/{id}
  // Deletes a specific job.
  public deleteJob(job: Job | string): Observable<Job> {
    const id = typeof job === 'string' ? job : (job as Job).id;
    return this.http.delete<Job>(`${environment.api}/jobs/${id}`);
  }
}

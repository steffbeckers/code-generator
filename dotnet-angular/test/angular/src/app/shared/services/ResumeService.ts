import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Resume } from 'src/app/shared/models/Resume';

@Injectable({ providedIn: 'root' })
export class ResumeService {
  constructor(private http: HttpClient) {}

  // GET: api/resumes
  // Retrieves all resumes.
  public getResumes(): Observable<Resume[]> {
    return this.http.get<Resume[]>(`${environment.api}/resumes`);
  }

  // GET: api/resumes/{id}
  // Retrieves a specific resume.
  public getResume(resume: Resume | string): Observable<Resume> {
    const id = typeof resume === 'string' ? resume : (resume as Resume).id;
    return this.http.get<Resume>(`${environment.api}/resumes/${id}`);
  }

  // POST: api/resumes
  // Creates a new resume.
  public createResume(resume: Resume): Observable<Resume> {
    return this.http.post<Resume>(`${environment.api}/resumes`, resume);
  }

  // PUT: api/resumes/{id}
  // Updates a specific resume.
  public updateResume(resume: Resume): Observable<Resume> {
    return this.http.put<Resume>(`${environment.api}/resumes/${resume.id}`, resume);
  }

  // PUT: api/Resumes/{resumeId}/skills/{skillId}/link
  // Links a specific skill to resume.
  public linkSkillToResume(resumeId: string, skillId: string): Observable<Resume> {
    return this.http.put<Resume>(`${environment.api}/resumes/${resumeId}/skills/${skillId}/link`, null);
  }

  // PUT: api/Resumes/{resumeId}/skills/{skillId}/unlink
  // Unlinks a specific skill from resume.
  public unlinkSkillFromResume(resumeId: string, skillId: string): Observable<Resume> {
    return this.http.put<Resume>(`${environment.api}/resumes/${resumeId}/skills/${skillId}/unlink`, null);
  }

  // DELETE: api/resumes/{id}
  // Deletes a specific resume.
  public deleteResume(resume: Resume | string): Observable<Resume> {
    const id = typeof resume === 'string' ? resume : (resume as Resume).id;
    return this.http.delete<Resume>(`${environment.api}/resumes/${id}`);
  }
}

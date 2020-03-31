import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { Skill } from 'src/app/shared/models/Skill';

@Injectable({ providedIn: 'root' })
export class SkillService {
  constructor(private http: HttpClient) {}

  // GET: api/skills
  // Retrieves all skills.
  public getSkills(): Observable<Skill[]> {
    return this.http.get<Skill[]>(`${environment.api}/skills`);
  }

  // GET: api/skills/{id}
  // Retrieves a specific skill.
  public getSkill(skill: Skill | string): Observable<Skill> {
    const id = typeof skill === 'string' ? skill : (skill as Skill).id;
    return this.http.get<Skill>(`${environment.api}/skills/${id}`);
  }

  // POST: api/skills
  // Creates a new skill.
  public createSkill(skill: Skill): Observable<Skill> {
    return this.http.post<Skill>(`${environment.api}/skills`, skill);
  }

  // PUT: api/skills/{id}
  // Updates a specific skill.
  public updateSkill(skill: Skill): Observable<Skill> {
    return this.http.put<Skill>(`${environment.api}/skills/${skill.id}`, skill);
  }

  // PUT: api/Skills/{skillId}/resumes/{resumeId}/link
  // Links a specific resume to skill.
  public linkResumeToSkill(skillId: string, resumeId: string): Observable<Skill> {
    return this.http.put<Skill>(`${environment.api}/skills/${skillId}/resumes/${resumeId}/link`, null);
  }

  // PUT: api/Skills/{skillId}/resumes/{resumeId}/unlink
  // Unlinks a specific resume from skill.
  public unlinkResumeFromSkill(skillId: string, resumeId: string): Observable<Skill> {
    return this.http.put<Skill>(`${environment.api}/skills/${skillId}/resumes/${resumeId}/unlink`, null);
  }

  // PUT: api/Skills/{skillId}/jobs/{jobId}/link
  // Links a specific job to skill.
  public linkJobToSkill(skillId: string, jobId: string): Observable<Skill> {
    return this.http.put<Skill>(`${environment.api}/skills/${skillId}/jobs/${jobId}/link`, null);
  }

  // PUT: api/Skills/{skillId}/jobs/{jobId}/unlink
  // Unlinks a specific job from skill.
  public unlinkJobFromSkill(skillId: string, jobId: string): Observable<Skill> {
    return this.http.put<Skill>(`${environment.api}/skills/${skillId}/jobs/${jobId}/unlink`, null);
  }

  // DELETE: api/skills/{id}
  // Deletes a specific skill.
  public deleteSkill(skill: Skill | string): Observable<Skill> {
    const id = typeof skill === 'string' ? skill : (skill as Skill).id;
    return this.http.delete<Skill>(`${environment.api}/skills/${id}`);
  }
}

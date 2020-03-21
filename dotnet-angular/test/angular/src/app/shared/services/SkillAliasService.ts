import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

// RxJS
import { Observable } from 'rxjs';

// Models
import { SkillAlias } from 'src/app/shared/models/SkillAlias';

@Injectable({ providedIn: 'root' })
export class SkillAliasService {
  constructor(private http: HttpClient) {}

  // GET: api/skillaliases
  // Retrieves all skillaliases.
  public getSkillAliases(): Observable<SkillAlias[]> {
    return this.http.get<SkillAlias[]>(`${environment.api}/skillaliases`);
  }

  // GET: api/skillaliases/{id}
  // Retrieves a specific skillalias.
  public getSkillAlias(skillAlias: SkillAlias | string): Observable<SkillAlias> {
    const id = typeof skillAlias === 'string' ? skillAlias : (skillAlias as SkillAlias).id;
    return this.http.get<SkillAlias>(`${environment.api}/skillaliases/${id}`);
  }

  // POST: api/skillaliases
  // Creates a new skillalias.
  public createSkillAlias(skillAlias: SkillAlias): Observable<SkillAlias> {
    return this.http.post<SkillAlias>(`${environment.api}/skillaliases`, skillAlias);
  }

  // PUT: api/skillaliases/{id}
  // Updates a specific skillalias.
  public updateSkillAlias(skillAlias: SkillAlias): Observable<SkillAlias> {
    return this.http.put<SkillAlias>(`${environment.api}/skillaliases/${skillAlias.id}`, skillAlias);
  }

  // DELETE: api/skillaliases/{id}
  // Deletes a specific skillalias.
  public deleteSkillAlias(skillAlias: SkillAlias | string): Observable<SkillAlias> {
    const id = typeof skillAlias === 'string' ? skillAlias : (skillAlias as SkillAlias).id;
    return this.http.delete<SkillAlias>(`${environment.api}/skillaliases/${id}`);
  }
}

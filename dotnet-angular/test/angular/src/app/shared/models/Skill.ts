import { SkillAlias } from './SkillAlias';
import { Resume } from './Resume';
import { Job } from './Job';

export class Skill {
  public id: string;
  public name: string;
  public description: string;
  public aliases: SkillAlias[];
  public resumes: Resume[];
  public jobs: Job[];
}

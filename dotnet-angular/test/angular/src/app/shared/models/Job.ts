import { JobState } from './JobState';
import { Skill } from './Skill';

export class Job {
  public id: string;
  public title: string;
  public description: string;
  public jobStateId: string;
  public jobState: JobState;
  public skills: Skill[];
}

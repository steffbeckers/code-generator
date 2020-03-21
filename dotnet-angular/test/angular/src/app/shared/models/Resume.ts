import { ResumeState } from './ResumeState';
import { Skill } from './Skill';

export class Resume {
  public id: string;
  public jobTitle: string;
  public description: string;
  public resumeStateId: string;
  public resumeState: ResumeState;
  public skills: Skill[];
}

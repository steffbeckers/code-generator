import { Resume } from './Resume';

export class Document {
  public id: string;
  public name: string;
  public displayName: string;
  public description: string;
  public path: string;
  public uRL: string;
  public mimeType: string;
  public resumes: Resume[];
}

import { Account } from './Account';

export class Note {
  public id: string;
  public title: string;
  public body: string;
  public accounts: Account[];
}

import { Account } from './Account';

export class Contact {
  public id: string;
  public firstName: string;
  public lastName: string;
  public website: string;
  public telephone: string;
  public email: string;
  public accountId: string;
  public account: Account;
}

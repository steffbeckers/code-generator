import { Account } from './Account';

export class Address {
  public id: string;
  public street: string;
  public number: string;
  public postalCode: string;
  public city: string;
  public primary: boolean;
  public accountId: string;
  public account: Account;
}

import { Address } from './Address';
import { Contact } from './Contact';

export class Account {
  public id: string;
  public name: string;
  public website: string;
  public telephone: string;
  public email: string;
  public addresses: Address[];
  public contacts: Contact[];
}

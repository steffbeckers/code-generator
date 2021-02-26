import { Account } from './account.model';

export class Contact {
  id: string;
  firstName: string;
  lastName: string;
  telephone: string;
  email: string;
  website: string;
  dateCreated: Date;
  dateModified: Date;

  account: Account;
  accountId: string;
}

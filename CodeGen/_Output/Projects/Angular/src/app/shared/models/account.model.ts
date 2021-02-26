import { Contact } from './contact.model';

export class Account {
  id: string;
  name: string;
  description: string;
  telephone: string;
  email: string;
  website: string;
  dateCreated: Date;
  dateModified: Date;

  contacts: Contact[] = [];
}

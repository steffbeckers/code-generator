export class Contact {
  id: string;
  firstName: string;
  lastName: string;
  jobTitle: string;
  email: string;
  telephone: string;
  mobilePhone: string;
  gender: CRM.API.Models.Gender;
  accountId: string;
  createdOn: Date;
  modifiedOn: Date;
  deletedOn: Date;
  createdById: string;
  modifiedById: string;
  account: Account;
}

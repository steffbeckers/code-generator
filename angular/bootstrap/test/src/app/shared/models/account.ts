export class Account {
  id: string;
  name: string;
  email: string;
  telephone: string;
  fax: string;
  website: string;
  vatNumber: string;
  description: string;
  addressId: string;
  parentAccountId: string;
  billingAccountId: string;
  relationTypeId: string;
  primaryContactId: string;
  createdOn: Date;
  modifiedOn: Date;
  deletedOn: Date;
  createdById: string;
  modifiedById: string;
  address: Address;
  parentAccount: Account;
  billingAccount: Account;
  relationType: AccountRelationType;
  primaryContact: Contact;
  createdBy: User;
  modifiedBy: User;
}

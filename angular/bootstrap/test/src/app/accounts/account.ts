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
  address: Address;
  parentAccount: Account;
  billingAccount: Account;
  relationType: RelationType;
  // primaryContact
  // contacts
  // workOrders
}

export class Address {
  id: string;
  street: string;
  number: string;
  city: string;
  postalCode: string;
}

export class RelationType {
  id: string;
  name: string;
}

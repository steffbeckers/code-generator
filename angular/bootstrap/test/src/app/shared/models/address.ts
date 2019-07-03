export class Address {
  id: string;
  street: string;
  number: string;
  city: string;
  state: string;
  postalCode: string;
  latitude: number;
  longitude: number;
  countryId: string;
  createdOn: Date;
  modifiedOn: Date;
  deletedOn: Date;
  createdById: string;
  modifiedById: string;
  country: Country;
}

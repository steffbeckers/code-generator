export class User {
  firstName: string;
  lastName: string;
  id: string;
  userName: string;
  email: string;
  emailConfirmed: bool;
  phoneNumber: string;
  phoneNumberConfirmed: bool;
  twoFactorEnabled: bool;
  lockoutEnd: Date;
  lockoutEnabled: bool;
  accessFailedCount: number;
}

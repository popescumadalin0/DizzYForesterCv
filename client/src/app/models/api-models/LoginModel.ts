export class LoginModel {
  username: string;
  password: string;

  constructor(value: LoginModel) {
    this.username = value.username;
    this.password = value.password;
  }
}

export class ResponseLoginModel {
  public userName: string;
  public token: string;
  public refreshToken: string;

  constructor(value: ResponseLoginModel) {
      this.userName=value.userName;
      this.token=value.token;
      this.refreshToken=value.refreshToken;
  }
}

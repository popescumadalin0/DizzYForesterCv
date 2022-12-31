import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { LoginModel } from 'src/app/models/api-models/LoginModel';
import { Constants } from 'src/app/models/Constants';
import { HttpService } from './http.service';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private isAdmin: Subject<boolean>;

  constructor(
    private httpService: HttpService,
    private tokenService: TokenService,
    private router: Router
  ) {}

  public loginUser(user: LoginModel) {
    this.httpService.post<LoginModel>(user, 'user/loginUser').subscribe(res => {
      this.setAdmin();
      this.saveUser(res);
      this.tokenService.saveToken(res.token);
      this.tokenService.saveRefreshToken(res.refreshToken);
      this.router.navigate(['', '/home']);
    });
  }
  public setAdmin() {
    this.isAdmin.next(true);
  }
  public saveUser(user: any): void {
    window.sessionStorage.removeItem(Constants.USER_KEY);
    window.sessionStorage.setItem(Constants.USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(Constants.USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
  signOut(): void {
    window.sessionStorage.clear();
  }
}

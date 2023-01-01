import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Subject, take } from 'rxjs';
import { LoginModel } from 'src/app/models/api-models/LoginModel';
import { Constants } from 'src/app/models/Constants';
import { HttpService } from './http.service';
import { TokenService } from './token.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  public logged$: BehaviorSubject<boolean> = new BehaviorSubject(false);

  constructor(
    private httpService: HttpService,
    private tokenService: TokenService,
    private router: Router
  ) {}

  public loginUser(user: LoginModel) {
    this.httpService
      .post<LoginModel>(user, 'user/loginUser')
      .pipe(take(1))
      .subscribe(res => {
        this.setLoggedEvent();
        this.saveUser(res);
        this.tokenService.saveToken(res.token);
        this.tokenService.saveRefreshToken(res.refreshToken);
        this.router.navigate(['/home']);
      });
  }
  public setLoggedEvent() {
    this.logged$.next(true);
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

    return null;
  }
  public signOut(): void {
    window.sessionStorage.clear();
    this.setLoggedEvent();
  }
  public isLogged() {
    if (this.getUser() != null) {
      return true;
    }
    return false;
  }
}

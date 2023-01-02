import { HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import {
  catchError,
  filter,
  finalize,
  retry,
  switchMap,
  take,
} from 'rxjs/operators';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';
import { ResponseLoginModel } from 'src/app/models/api-models/ResponseLoginModel';
import { ToastrService } from 'ngx-toastr';
import { Constants } from 'src/app/models/Constants';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(
    null
  );

  constructor(
    private tokenService: TokenService,
    private authService: AuthService,
    private toastr: ToastrService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    let authReq = req;
    const token = this.tokenService.getToken();
    if (token != null) {
      authReq = this.addTokenHeader(req, token);
    }
    return next.handle(authReq).pipe(
      retry(0),
      catchError(error => {
        if (
          error instanceof HttpErrorResponse &&
          !authReq.url.includes('sysadmin/login') &&
          error.status === 401 &&
          token
        ) {
          return this.handle401Error(authReq, next, error);
        }
        this.toastr.error(error.error.message);
        return throwError(error);
      })
    );
  }

  private handle401Error(
    request: HttpRequest<any>,
    next: HttpHandler,
    error: any
  ) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);
      const token = this.tokenService.getRefreshToken();
      const refreshToken = this.tokenService.getRefreshToken();
      const user = this.authService.getUser();
      if (token) {
        return this.tokenService
          .refreshToken(
            new ResponseLoginModel({
              userName: user.userName,
              token: token,
              refreshToken: refreshToken,
            })
          )
          .pipe(
            switchMap((token: any) => {
              this.isRefreshing = false;

              this.tokenService.saveToken(token.token);
              this.tokenService.saveRefreshToken(token.refreshToken);
              this.refreshTokenSubject.next(token.token);
              return next.handle(this.addTokenHeader(request, token.token));
            }),
            catchError(err => {
              this.isRefreshing = false;
              console.log(err);
              this.authService.signOut();
              this.toastr.error(err.error.message);
              return throwError(err);
            })
          );
      }
    } else {
      return throwError(error);
    }

    return this.refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap(token => {
        return next.handle(this.addTokenHeader(request, token));
      })
    );
  }

  private addTokenHeader(request: HttpRequest<any>, token: string) {
    return request.clone({
      headers: request.headers.set(
        Constants.TOKEN_HEADER_KEY,
        'Bearer ' + token
      ),
    });
  }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpService } from './http.service';
import { Constants } from 'src/app/models/Constants';
import { ResponseLoginModel } from 'src/app/models/api-models/ResponseLoginModel';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  constructor(private httpService: HttpService) {}

  refreshToken(tokens: ResponseLoginModel) {
    return this.httpService.post<ResponseLoginModel>(tokens,'token/refreshToken');
  }
  public saveToken(token: string): void {
    window.sessionStorage.removeItem(Constants.TOKEN_KEY);
    window.sessionStorage.setItem(Constants.TOKEN_KEY, token);
  }

  public getToken(): string | null {
    return window.sessionStorage.getItem(Constants.TOKEN_KEY);
  }

  public saveRefreshToken(token: string): void {
    window.sessionStorage.removeItem(Constants.REFRESHTOKEN_KEY);
    window.sessionStorage.setItem(Constants.REFRESHTOKEN_KEY, token);
  }

  public getRefreshToken(): string | null {
    return window.sessionStorage.getItem(Constants.REFRESHTOKEN_KEY);
  }
}

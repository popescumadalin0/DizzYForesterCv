import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { map, Observable, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TokenService } from './token.service';
import { Constants } from 'src/app/models/Constants';
import { HttpResponseModel } from 'src/app/models/api-models/HttpResponseModel';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly APIUrl = environment.config.apiUrl;

  constructor(protected httpClient: HttpClient) {}

  get<T>(id: string | number, api: string): Observable<T> {
    return this.httpClient
      .get<HttpResponseModel>(`${this.APIUrl}/${api}/${id}`)
      .pipe(
        map((res: HttpResponseModel) => {
          return res?.value;
        })
      );
  }

  post<T>(resource: T, api: string): Observable<any> {
    return this.httpClient.post(`${this.APIUrl}/${api}`, resource).pipe(
      map((res: HttpResponseModel) => {
        return res?.value;
      })
    );
  }

  delete(id: string | number, api: string): Observable<any> {
    return this.httpClient.delete(`${this.APIUrl}/${api}/${id}`).pipe(
      map((res: HttpResponseModel) => {
        return res?.value;
      })
    );
  }

  put<T>(resource: T, api: string): Observable<any> {
    return this.httpClient.put(`${this.APIUrl}/${api}`, resource).pipe(
      map((res: HttpResponseModel) => {
        return res?.value;
      })
    );
  }
}

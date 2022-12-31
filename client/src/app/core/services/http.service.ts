import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class HttpService {
  private readonly APIUrl = environment.config.apiUrl;

  constructor(protected httpClient: HttpClient) {}

  get<T>(id: string | number, api: string): Observable<T> {
    return this.httpClient.get<T>(this.APIUrl + '/' + api + +'/' + id);
  }

  post<T>(resource: T, api: string): Observable<any> {
    return this.httpClient.post(this.APIUrl + '/' + api, resource);
  }

  delete(id: string | number, api: string): Observable<any> {
    return this.httpClient.delete(`/${this.APIUrl}}/${api}}/${id}`);
  }

  put<T>(resource: T, api: string) {
    return this.httpClient.put(`/${this.APIUrl}}/${api}`, resource);
  }
}

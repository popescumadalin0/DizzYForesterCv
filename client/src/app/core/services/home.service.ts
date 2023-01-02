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
export class HomeService {

  constructor(
    private httpService: HttpService,
    private router: Router
  ) {}
  public onClick(){
    this.httpService.get('','home/getHome').subscribe();
  }
}

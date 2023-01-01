import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../services/auth.service';
@Injectable()
export class AuthGuard implements CanActivate {
  constructor(
    public auth: AuthService,
    public router: Router,
    public toastr: ToastrService
  ) {}
  canActivate(): boolean {
    if (this.auth.isLogged()) {
      this.toastr.warning('You are logged!');
      this.router.navigate(['home']);
      return false;
    }
    return true;
  }
}

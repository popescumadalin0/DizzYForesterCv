import { Component, ElementRef } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { LoginModel } from 'src/app/models/api-models/LoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginGroup: FormGroup;
  constructor(builder: FormBuilder, private authService: AuthService) {
    this.loginGroup = builder.group({
      usernameControl: new FormControl('', Validators.required),
      passwordControl: new FormControl('', Validators.required),
    });
  }
  onLogin() {
    this.authService.loginUser(
      new LoginModel({
        username: this.loginGroup.controls['usernameControl'].value,
        password: this.loginGroup.controls['passwordControl'].value,
      })
    );
  }
}

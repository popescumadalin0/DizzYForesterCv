import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent extends BaseComponent {
  constructor(authService: AuthService) {
    super(authService);
  }
}

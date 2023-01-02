import { Component } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { HomeService } from 'src/app/core/services/home.service';
import { BaseComponent } from '../base/base.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent extends BaseComponent {
  onInit() {
  }
  onDestroy() {
  }
  constructor(authService: AuthService,private homeService: HomeService) {
    super(authService);
  }
  onClick(){
    this.homeService.onClick();
  }
}

import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Constants } from './models/Constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'ro']);
    if (localStorage.getItem(Constants.LANGUAGE_KEY)) {
      translate.use(localStorage.getItem(Constants.LANGUAGE_KEY));
      translate.setDefaultLang(localStorage.getItem(Constants.LANGUAGE_KEY));
    } else {
      translate.use(translate.defaultLang);
      translate.setDefaultLang('en');
    }
  }
  ngOnInit(): void {}
}

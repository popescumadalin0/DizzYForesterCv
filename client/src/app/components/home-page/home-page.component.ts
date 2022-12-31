import { Component, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent {
  elementRef: ElementRef;
  constructor(elementRef: ElementRef) {
    this.elementRef = elementRef;
  }
}

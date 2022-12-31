import { Component, ElementRef } from '@angular/core';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.scss']
})
export class SkillsComponent {
  elementRef: ElementRef;
  constructor(elementRef: ElementRef) {
    this.elementRef = elementRef;
  }
}

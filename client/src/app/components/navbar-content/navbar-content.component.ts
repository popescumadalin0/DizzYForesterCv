import { Component, OnInit } from '@angular/core';
import {
  ActivatedRoute,
  Router,
} from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-navbar-content',
  templateUrl: './navbar-content.component.html',
  styleUrls: ['./navbar-content.component.scss'],
})
export class NavbarContentComponent implements OnInit {
  currentSection = 'home';
  navLinks: any[];
  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private _location: Location
  ) {}
  ngOnInit() {
    this.navLinks = [
      {
        label: 'Home',
        section: 'home',
      },
      {
        label: 'Skills',
        section: 'skills',
      },
      {
        label: 'Projects',
        section: 'projects',
      },
    ];
    this._route.params.subscribe(params => {
      this.currentSection = params['sectionId'];
      this.scrollTo(this.currentSection);
    });
  }
  onSectionChange(sectionId: string) {
    this._location.go(sectionId);
    this.currentSection = sectionId;
  }

  scrollTo(section) {
    document.querySelector('#' + section).scrollIntoView();
  }
}

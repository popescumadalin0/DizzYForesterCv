import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss'],
})
export abstract class BaseComponent implements OnInit, OnDestroy {
  private subscription$: Subject<boolean> = new Subject();
  public isLogged = false;

  constructor(protected authService: AuthService) {}
  ngOnDestroy(): void {
    this.subscription$.next(true);
    this.subscription$.complete();
    this.onDestroy();
  }
  ngOnInit(): void {
    this.authService.logged$
      .pipe(takeUntil(this.subscription$))
      .subscribe(res => {
        this.isLogged = res;
      });
    this.onInit();
  }
  abstract onInit();
  abstract onDestroy();
}

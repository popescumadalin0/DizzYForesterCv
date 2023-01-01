import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss'],
})
export class BaseComponent implements OnDestroy {
  private subscription$: Subject<boolean> = new Subject();
  public isLogged = false;
  constructor(authService: AuthService) {
   authService.logged$
      .pipe(takeUntil(this.subscription$))
      .subscribe(res => {
        this.isLogged = authService.isLogged();
      });
  }

  ngOnDestroy(): void {
    this.subscription$.next(true);
    this.subscription$.complete();
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subject, takeUntil } from 'rxjs';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss'],
})
export class SpinnerComponent implements OnInit, OnDestroy {
  private _subscription: Subject<boolean> = new Subject();
  public isLoading = false;

  constructor(public loader: LoaderService) {}
  ngOnInit(): void {
    this.loader.loading$.pipe(takeUntil(this._subscription)).subscribe(res => {
      this.isLoading = res;
    });
  }
  ngOnDestroy(): void {
    this._subscription.next(true);
    this._subscription.complete();
  }
}

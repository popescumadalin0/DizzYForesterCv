import { combineLatest, debounceTime, map, Observable, startWith } from "rxjs";
import * as _ from 'lodash';

export function dirtyCheck<U>(source: Observable<U>) {
  return function <T>(valueChanges: Observable<T>): Observable<boolean> {
    const isDirty$ = combineLatest(source, valueChanges).pipe(
      debounceTime(300),
      map(([a, b]) => _.isEqual(a, b) === false),
      startWith(false)
    );

    return isDirty$;
  };
}


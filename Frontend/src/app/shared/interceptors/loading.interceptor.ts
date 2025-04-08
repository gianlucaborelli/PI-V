import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { SpinnerService } from '../services/spinner.service';

let totalRequests = 0;

export const loadingInterceptorFn: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(SpinnerService);

  totalRequests++;
  loadingService.setLoading(true);

  return next(req).pipe(
    finalize(() => {
      totalRequests--;
      if (totalRequests === 0) {
        loadingService.setLoading(false);
      }
    })
  );
};

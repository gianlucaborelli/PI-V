import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
  HttpErrorResponse,
  HttpEvent
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from '../../authentication/service/auth.service';
import { RefreshTokenModel } from '../../authentication/models/refresh-token.model';
import { TokenApiModel } from '../../authentication/models/token-api.model';
import { SnackBarService } from '../services/snack-bar.service';

let refreshingToken = false;

export const tokenInterceptorFn: HttpInterceptorFn = (
  request: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const auth = inject(AuthService);
  const snackbarService = inject(SnackBarService);

  const myToken = auth.getToken();

  if (myToken) {
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${myToken}`
      }
    });
  }

  return next(request).pipe(
    catchError((err: any) => {
      if (err instanceof HttpErrorResponse && err.status === 401) {
        return handleUnauthorizedError(request, next, auth, snackbarService);
      } else {
        return throwError(() => err);
      }
    })
  );
};

function handleUnauthorizedError(
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  auth: AuthService,
  snackbarService: SnackBarService
): Observable<HttpEvent<any>> {
  if (!refreshingToken) {
    refreshingToken = true;

    const tokenApiModel = new RefreshTokenModel();
    tokenApiModel.refreshToken = auth.getRefreshToken()!;

    return auth.renewToken(tokenApiModel).pipe(
      switchMap((data: TokenApiModel) => {
        auth.storeToken(data.accessToken);
        auth.storeRefreshToken(data.refreshToken);

        refreshingToken = false;
        const newRequest = request.clone({
          setHeaders: {
            Authorization: `Bearer ${data.accessToken}`
          }
        });

        return next(newRequest);
      }),
      catchError((err) => {
        refreshingToken = false;

        if (err instanceof HttpErrorResponse && err.status === 401) {
          snackbarService.open('Token expirado');
        } else {
          console.error(err);
          snackbarService.open('Erro ao renovar o token');
        }

        auth.logout();
        return throwError(() => err);
      })
    );
  } else {
    return throwError(() => new HttpErrorResponse({ status: 401, statusText: 'Unauthorized' }));
  }
}

import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { SharedService } from './shared.service';
import { Router } from '@angular/router';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(public SharedService: SharedService,private router:Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let token = this.SharedService.GetToken();
    if (token != null) {
      request = request.clone({headers: request.headers.set('Authorization',`Bearer ${token}`),  });
    }
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err.status === 401 ||err.status === 204) {
          // auto logout if 401 response returned from api
          this.SharedService.Logout();
          this.router.navigate(['login']);
        }
        const error = err.error?.message || err.statusText;
        return throwError(error);
      })
      ) as Observable<HttpEvent<unknown>>;
    }
  }

import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { User } from '../models/user';
import { RegistrationService } from '../services/registration.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private regService: RegistrationService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let currentUser;

    this.regService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);

    if (currentUser)
    {
      const user = currentUser as User;
      request = request.clone({
        headers: request.headers.set("Authorization", `Bearer ${user.token}`)
      });
    }

    return next.handle(request);
  }
}

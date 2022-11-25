import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable, take } from 'rxjs';
import { User } from '../models/user';
import { RegistrationService } from '../services/registration.service';
import { ToastService } from '../services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {
  constructor(private regService: RegistrationService,
    private toast: ToastService) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      return this.regService.currentUser$.pipe(
        map(user => {
          if (user?.role === 'admin') return true;
          this.toast.showError("Cannot enter this area");
          return false;
        })
      );
  }
}

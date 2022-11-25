import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { RegistrationService } from '../services/registration.service';
import { ToastService } from '../services/toast.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private regService: RegistrationService,
    private toast: ToastService) {

  }
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
      return this.regService.currentUser$.pipe(
        map(user => {
          if (user) return true;
          this.toast.showError("You are not authenticated");
          return false;
        })
      );
  }
  
}

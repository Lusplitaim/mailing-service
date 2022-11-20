import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  baseApi = environment.baseApi;
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { 

  }
  
  createUser(user: User) {
    return this.http.post<User>(`${this.baseApi}/Accounts/SignUp`, user);
  }

  signIn(email: string, password: string) {
    return this.http.post<User>(`${this.baseApi}/Accounts/SignIn`, { email, password }).pipe(
      map((user: User) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  signOut() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}

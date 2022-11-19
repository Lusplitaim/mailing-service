import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {
  baseApi = environment.baseApi;

  constructor(private http: HttpClient) { 

  }
  
  createUser(user: User) {
    return this.http.post<User>(`${this.baseApi}/Accounts/SignUp`, user);
  }
}

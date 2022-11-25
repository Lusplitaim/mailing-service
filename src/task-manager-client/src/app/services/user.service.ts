import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseApi = environment.baseApi;

  constructor(private http: HttpClient) { }

  getUsers() {
    return this.http.get<User[]>(`${this.baseApi}/Users/GetUsers`);
  }
}

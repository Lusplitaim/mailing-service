import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { take } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { CronTask } from '../models/cron-task';
import { User } from '../models/user';
import { RegistrationService } from './registration.service';

@Injectable({
  providedIn: 'root'
})
export class CronTaskService {
  baseApi = environment.baseApi;

  constructor(private http: HttpClient,
    private regService: RegistrationService) { }

  createTask(cronTask: CronTask) {
    return this.http.post(`${this.baseApi}/Tasks/CreateTask`, cronTask);
  }

  getUserTasks() {
    return this.http.get<CronTask[]>(`${this.baseApi}/Tasks/GetTasksByUsername`);
  }

  getTasksByUserId(userId: string) {
    return this.http.get<CronTask[]>(`${this.baseApi}/Tasks/GetTasksByUserId/${userId}`);
  }

  deleteTask(id: number) {
    return this.http.delete<boolean>(`${this.baseApi}/Tasks/DeleteTask/${id}`);
  }
}

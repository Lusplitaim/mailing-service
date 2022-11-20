import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { CronTask } from '../models/cron-task';

@Injectable({
  providedIn: 'root'
})
export class CronTaskService {
  baseApi = environment.baseApi;

  constructor(private http: HttpClient) { }

  createTask(cronTask: CronTask) {
    return this.http.post<boolean>(`${this.baseApi}/Tasks/CreateTask`, cronTask);
  }
}

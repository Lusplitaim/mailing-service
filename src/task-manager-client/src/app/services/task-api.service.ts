import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { TaskApi } from '../models/task-api';
import { UrlPath } from '../models/url-path';

@Injectable({
  providedIn: 'root'
})
export class TaskApiService {
  baseApi = environment.baseApi;

  constructor(private http: HttpClient) { }

  getApis() {
    return this.http.get<TaskApi[]>(`${this.baseApi}/Apis/GetApis`);
  }

  getApiPaths(apiId: number) {
    return this.http.get<UrlPath[]>(`${this.baseApi}/UrlPaths/GetPathsByApiId/${apiId}`);
  }
}

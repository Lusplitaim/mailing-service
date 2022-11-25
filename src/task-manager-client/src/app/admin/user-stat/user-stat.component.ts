import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CronTask } from 'src/app/models/cron-task';
import { TaskApi } from 'src/app/models/task-api';
import { User } from 'src/app/models/user';
import { CronTaskService } from 'src/app/services/cron-task.service';
import { TaskApiService } from 'src/app/services/task-api.service';

@Component({
  selector: 'app-user-stat',
  templateUrl: './user-stat.component.html',
  styleUrls: ['./user-stat.component.css']
})
export class UserStatComponent implements OnInit {
  tasks!: CronTask[];
  apis!: TaskApi[];

  constructor(private taskService: CronTaskService,
    private apiService: TaskApiService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.getUserTasks();
    this.getApis();
  }

  getUserTasks() {
    const userId = this.route.snapshot.paramMap.get('userId');
    this.taskService.getTasksByUserId(userId!).subscribe(tasks => {
      this.tasks = tasks;
    });
  }

  getApis() {
    this.apiService.getApis().subscribe(apis => {
      this.apis = apis;
    });
  }

  getApiUseCount(apiId: number) {
    let useCount = 0;
    this.tasks.forEach((t) => {
      if (t.apiId === apiId) useCount += t.executionCount;
    });
    return useCount;
  }

  getTotalApiUseCount() {
    let totalUseCount = 0;
    for (let task of this.tasks) {
      totalUseCount += task.executionCount;
    }
    return totalUseCount;
  }
}

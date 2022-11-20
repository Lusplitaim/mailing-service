import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { CronTask } from 'src/app/models/cron-task';
import { TaskApi } from 'src/app/models/task-api';
import { User } from 'src/app/models/user';
import { CronTaskService } from 'src/app/services/cron-task.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { TaskApiService } from 'src/app/services/task-api.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-task-creation',
  templateUrl: './task-creation.component.html',
  styleUrls: ['./task-creation.component.css']
})
export class TaskCreationComponent implements OnInit {
  currentUser: User | null = null;

  services: string[] = ['WizardWorldApi', 'DogFacts', 'FreeDictionaryApi'];

  taskApis: TaskApi[] = [];

  activeApiTab: string = this.services[0];

  taskForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private taskApiService: TaskApiService,
    private toast: ToastService,
    private regService: RegistrationService,
    private cronTaskService: CronTaskService,
    private router: Router) {

    this.regService.currentUser$.pipe(take(1)).subscribe(user => {
      this.currentUser = user;
    });

    this.taskForm = this.formBuilder.group({
      name: [null, [ Validators.required ] ],
      description: [null, [ Validators.required ] ],
      minutes: [[], [ Validators.required ] ],
      hours: [[], [ Validators.required ] ],
      days: [[], [ Validators.required ] ],
      months: [[], [ Validators.required ] ],
      weekdays: [[], [ Validators.required ] ],
    });
  }

  ngOnInit(): void {
    this.getTaskApis();
  }

  getTaskApis() {
    this.taskApiService.getApis().subscribe(apis => {
      this.taskApis = apis;
    });
  }

  createTask() {
    let apiName: string = this.activeApiTab;
    const taskApi = this.getTaskApiByName(apiName);
    if (!taskApi) {
      this.toast.showError("No api with such name");
      return;
    }

    let cronTask: CronTask = {
      id: 0,
      name: this.taskForm.value['name'],
      description: this.taskForm.value['description'],
      minutes: this.taskForm.value['minutes'].toString(),
      hours: this.taskForm.value['hours'].toString(),
      days: this.taskForm.value['days'].toString(),
      months: this.taskForm.value['months'].toString(),
      weekdays: this.taskForm.value['weekdays'].toString(),
      userId: this.currentUser!.id,
      apiId: taskApi.id
    };

    this.cronTaskService.createTask(cronTask).subscribe((isCreated: boolean) => {
      this.toast.showSuccess("New task has been created");
      this.router.navigateByUrl("/");
    }, error => {
      this.toast.showError(error);
    });
  }

  getTaskApiByName(apiName: string): TaskApi | undefined {
    return this.taskApis.find((taskApi) => taskApi.name === apiName);
  }

}

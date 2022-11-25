import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { CronTask } from 'src/app/models/cron-task';
import { TaskApi } from 'src/app/models/task-api';
import { UrlPath } from 'src/app/models/url-path';
import { User } from 'src/app/models/user';
import { CronTaskService } from 'src/app/services/cron-task.service';
import { RegistrationService } from 'src/app/services/registration.service';
import { TaskApiService } from 'src/app/services/task-api.service';
import { ToastService } from 'src/app/services/toast.service';
import { UrlParamsSetupComponent } from '../url-params-setup/url-params-setup.component';

@Component({
  selector: 'app-task-creation',
  templateUrl: './task-creation.component.html',
  styleUrls: ['./task-creation.component.css']
})
export class TaskCreationComponent implements OnInit {
  @ViewChild(UrlParamsSetupComponent) urlParamsSetup!: UrlParamsSetupComponent;

  currUrlPath: UrlPath | null = null;

  currentUser: User | null = null;

  taskApis: TaskApi[] = [];

  activeApi!: TaskApi;

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
      api: [null, [ Validators.required ]],
      urlPath: [null, [ Validators.required ]],
      name: [null, [ Validators.required ] ],
      description: [null],
      minutes: [[], [ Validators.required ] ],
      hours: [[], [ Validators.required ] ],
      days: [[], [ Validators.required ] ],
      months: [[], [ Validators.required ] ],
      weekdays: [[], [ Validators.required ] ],
      urlParamsString: [''],
    });

    this.onApiValueChanges();
    this.onUrlPathValueChanges();
  }

  ngOnInit(): void {
    this.getTaskApis();
  }

  openUrlParamsSetup() {
    this.urlParamsSetup.open();
  }

  createAndSetUrlParamsString(queryString: string) {
    let urlParamsString = `${this.taskForm.value['urlPath'].name}?${queryString}`;
    this.taskForm.controls['urlParamsString'].setValue(urlParamsString);
  }

  onApiValueChanges() {
    this.taskForm.get('api')!.valueChanges.subscribe((selectedApi: TaskApi) => {
      this.resetUrlPath();
      this.activeApi = selectedApi;
      if (selectedApi) {
        if (!selectedApi.urlPaths) {
          this.taskApiService.getApiPaths(selectedApi.id).subscribe(urlPaths => {
            selectedApi.urlPaths = urlPaths;
          });
        }
      }
    });
  }

  resetUrlPath() {
    this.taskForm.controls['urlPath'].setValue(null);
    this.currUrlPath = null;
  }

  onUrlPathValueChanges() {
    this.taskForm.get('urlPath')!.valueChanges.subscribe((selectedPath: UrlPath) => {
      this.currUrlPath = selectedPath;
      if (selectedPath) {
        if (!selectedPath?.urlParams) {
          this.taskApiService.getUrlPathParams(selectedPath.id).subscribe(urlParams => {
            selectedPath.urlParams = urlParams;
            this.currUrlPath = selectedPath;
          });
        }
      }
    });
  }

  getTaskApis() {
    this.taskApiService.getApis().subscribe(apis => {
      this.taskApis = apis;
    });
  }

  getPathsForSelectedApi() {
    let selectedApi = this.taskForm.value['api']?.value;
    if (selectedApi) {
      console.log(this.taskForm.value['api'].value);
    }
  }

  createTask() {
    let apiName: string = this.activeApi.name;
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
      apiId: taskApi.id,
      urlParamsString: this.taskForm.value['urlParamsString'],
      executionCount: 0
    };

    this.cronTaskService.createTask(cronTask).subscribe(() => {
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

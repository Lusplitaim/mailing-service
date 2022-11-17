import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-task-creation',
  templateUrl: './task-creation.component.html',
  styleUrls: ['./task-creation.component.css']
})
export class TaskCreationComponent implements OnInit {

  services = ['FreeDictionary', 'DogFacts', 'WizardWorld'];

  activeApiTab: string = this.services[0];

  taskForm: FormGroup;

  constructor(private formBuilder: FormBuilder) { 
    this.taskForm = this.formBuilder.group({
      email: [null, [ Validators.required, Validators.email ] ],
      api:  [this.activeApiTab, [ Validators.required ] ],
      minutes: [[], [ Validators.required ] ],
      hours: [[], [ Validators.required ] ],
      days: [[], [ Validators.required ] ],
      months: [[], [ Validators.required ] ],
      weekdays: [[], [ Validators.required ] ],
    });
  }

  ngOnInit(): void {
  }

  createTask() {
    let taskDetails: any = {};
    taskDetails.email = this.taskForm.value['email'];
    taskDetails.api = this.taskForm.value['api'];

    taskDetails.minutes = this.taskForm.value['minutes'].toString();
    taskDetails.hours = this.taskForm.value['hours'].toString();
    taskDetails.days = this.taskForm.value['days'].toString();
    taskDetails.months = this.taskForm.value['months'].toString();
    taskDetails.weekdays = this.taskForm.value['weekdays'].toString();

    console.log(taskDetails);
  }

}

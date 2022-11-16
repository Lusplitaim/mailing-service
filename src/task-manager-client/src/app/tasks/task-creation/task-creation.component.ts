import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-task-creation',
  templateUrl: './task-creation.component.html',
  styleUrls: ['./task-creation.component.css']
})
export class TaskCreationComponent implements OnInit {

  services = ['FreeDictionary', 'DogFacts', 'WizardWorld'];

  activeApiTab: string = this.services[0];

  constructor() { }

  ngOnInit(): void {
  }

}

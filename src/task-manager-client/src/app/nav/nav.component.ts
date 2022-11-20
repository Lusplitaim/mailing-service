import { Component, OnInit } from '@angular/core';
import { RegistrationService } from '../services/registration.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  constructor(public regService: RegistrationService) { }

  ngOnInit() {
  }

  signOut() {
    this.regService.signOut();
  }
}

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from 'src/app/services/registration.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private regService: RegistrationService) { 
    this.signUpForm = this.formBuilder.group({
      name: [null, [Validators.required] ],
      email: [null, [Validators.required, Validators.email] ],
      password: [null, [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  createUser() {
    this.regService.createUser(this.signUpForm.value).subscribe((user) => {
      console.log(user);
    });
  }

}

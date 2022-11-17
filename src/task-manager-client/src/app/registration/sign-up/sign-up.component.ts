import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(private formBuilder: FormBuilder) { 
    this.signUpForm = this.formBuilder.group({
      name: [null, [Validators.required] ],
      email: [null, [Validators.required, Validators.email] ],
      password: [null, [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

}

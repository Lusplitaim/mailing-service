import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Route, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { RegistrationService } from 'src/app/services/registration.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {

  signInForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private regService: RegistrationService,
    private toastService: ToastService,
    private router: Router) {

    this.signInForm = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email] ],
      password: [null, [Validators.required] ]
    });

  }

  ngOnInit() {
  }

  signIn() {
    const email = this.signInForm.value.email;
    const password = this.signInForm.value.password;
    this.regService.signIn(email, password).subscribe(user => {
      this.router.navigateByUrl("/");
    });
  }

}

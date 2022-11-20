import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistrationService } from 'src/app/services/registration.service';
import { ToastService } from 'src/app/services/toast.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  signUpForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private regService: RegistrationService,
    private toastService: ToastService) {

    this.signUpForm = this.formBuilder.group({
      username: [null, [Validators.required] ],
      email: [null, [Validators.required, Validators.email] ],
      password: [null, [Validators.required] ]
    });

  }

  ngOnInit(): void {
  }

  createUser() {
    this.regService.createUser(this.signUpForm.value).subscribe(
      (user) => {
        
      },
      (errorMsg) => {
        this.toastService.showError("Error occurred");
      }
    );
  }

}

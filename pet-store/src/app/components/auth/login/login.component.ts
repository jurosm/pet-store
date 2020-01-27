import { Component, OnInit, NgModule } from '@angular/core';
import { ErrorResponse } from 'src/app/models/error/errorResponse';
import { faUser, faKey } from '@fortawesome/free-solid-svg-icons';
import { LoginModel } from '../../../models/auth/loginModel';
import { PetStoreService } from '../../../services/pet-store.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, FormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

@NgModule({
  imports: [
      FormGroup, FormControl, ReactiveFormsModule, CommonModule,
      FormsModule
  ]
})


export class LoginComponent implements OnInit {

  faUser: any;
  faKey: any;

  login: LoginModel;

  loginForm: FormGroup;

  serverError: ErrorResponse;

  constructor(private service: PetStoreService, private router: Router) {
    this.faUser = faUser;
    this.faKey = faKey;
    this.login = new LoginModel();

    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.compose([Validators.required]))
    });

    this.serverError = new ErrorResponse();
  }

  ngOnInit() {

  }

  submitLogin() {
    if (this.loginForm.controls.password.valid && this.loginForm.controls.username.valid) {
    this.service.login(this.login).subscribe(res => {

      localStorage.setItem('jwtToken', res.jwtToken);
      this.router.navigate(['/']);

    }, err => {
      this.serverError = err;
    });
  }
}

}

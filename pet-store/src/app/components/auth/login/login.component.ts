import { Component } from '@angular/core'
import { ErrorResponse } from '../../../../../src/app/models/error/errorResponse'
import { faUser, faKey, IconDefinition } from '@fortawesome/free-solid-svg-icons'
import { LoginModel } from '../../../models/auth/loginModel'
import { PetStoreService } from '../../../services/pet-store.service'
import { Router } from '@angular/router'
import { FormControl, FormGroup, Validators } from '@angular/forms'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  faUser: IconDefinition
  faKey: IconDefinition

  login: LoginModel

  loginForm: FormGroup

  serverError: ErrorResponse

  constructor(private readonly service: PetStoreService, private readonly router: Router) {
    this.faUser = faUser
    this.faKey = faKey
    this.login = new LoginModel()

    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.compose([Validators.required])),
    })

    this.serverError = new ErrorResponse()
  }

  submitLogin() {
    if (this.loginForm.controls.password.valid && this.loginForm.controls.username.valid) {
      this.service.login(this.login).subscribe({
        next: res => {
          localStorage.setItem('jwtToken', res.jwtToken)
          this.router.navigate(['/'])
        },
        error: err => {
          this.serverError = err
        },
      })
    }
  }
}

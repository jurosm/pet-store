import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Order } from '../models/order';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginModel } from '../models/auth/loginModel';
import { AuthResult } from '../models/auth/authResult';
import { ErrorService } from './errorService';

@Injectable({
  providedIn: 'root'
})

export class PetStoreService {

  url: string;
  constructor(private httpClient: HttpClient, private errorService: ErrorService) {
    this.url = 'https://localhost:5001/';
   }

  buy(order: Order) {
    return this.httpClient.post(this.url + 'orders/buy', order);
  }

  login(model: LoginModel): Observable<(AuthResult)> {
    return this.httpClient.post<AuthResult>(this.url + 'auth/login', model).pipe(
      catchError(this.errorService.handlerError)
    );
  }
}

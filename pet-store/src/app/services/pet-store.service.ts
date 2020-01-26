import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import { Order } from '../models/order';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { LoginModel } from '../models/auth/loginModel';
import { AuthResult } from '../models/auth/authResult';
import { ErrorService } from './errorService';
import { getLocaleExtraDayPeriods } from '@angular/common';
import { GetToysParams } from '../models/toy/getToysParams';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export class PetStoreService {

  url: string;
  constructor(private httpClient: HttpClient, private errorService: ErrorService) {
    this.url = environment.apiUrl;
   }

  buy(order: Order) {
    return this.httpClient.post(this.url + 'orders/buy', order);
  }

  login(model: LoginModel): Observable<(AuthResult)> {
    return this.httpClient.post<AuthResult>(this.url + 'auth/login', model).pipe(
      catchError(this.errorService.handlerError)
    );
  }

  getToys(getToysParams: GetToysParams) {
    const params = new HttpParams()
    .set('page', getToysParams.page.toString())
    .set('pageSize', getToysParams.itemsPerPage.toString());

    if(getToysParams.order !== '') { params.set('order', getToysParams.order); }
    if(getToysParams.category !== '') {  params.set('category', getToysParams.category); }
    if(getToysParams.matchName !== '') { params.set('match', getToysParams.matchName); }

    return this.httpClient.get(this.url + 'toys', {
      params
    });
  }
}

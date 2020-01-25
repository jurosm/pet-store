import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Order } from '../models/order';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PetStoreService {

  url: string;
  constructor(private httpClient: HttpClient) {
    this.url = 'https://localhost:5001/';
   }

  buy(order: Order) {
    return this.httpClient.post(this.url + 'orders/buy', order);
  }
}

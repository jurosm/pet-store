import { Injectable } from '@angular/core';
import { Order } from '../models/order';
import { PetStoreService } from './pet-store.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  order: Order;
  constructor(private service: PetStoreService) {
    this.order = new Order();
   }

  buy(){
    this.service.buy(this.order).subscribe();
  }

}

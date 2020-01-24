import { Injectable } from '@angular/core';
import { Order } from '../models/order';
import { PetStoreService } from './pet-store.service';

@Injectable({
  providedIn: 'root'
})
export class OrderServiceService {
  order:Order;
  constructor(private service:PetStoreService) { }

}

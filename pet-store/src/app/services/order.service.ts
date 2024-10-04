import { Injectable } from '@angular/core'
import { Observable } from 'rxjs'
import { catchError } from 'rxjs/operators'

import { Order } from '../models/order/order'
import { ErrorService } from './errorService'
import { PetStoreService } from './pet-store.service'

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(
    private readonly service: PetStoreService,
    private readonly errorService: ErrorService
  ) {}

  /**
   * Creates an order on the backend
   */
  create(order: Order): Observable<Order> {
    return this.service.create(order).pipe(catchError(this.errorService.handlerError))
  }

  /**
   * Creates the Stripe payment intent for the order
   * @param orderId Id of the order
   * @returns 
   */
  createPaymentIntent(orderId: number): Observable<any> {
    return this.service
      .createPaymentIntent(orderId)
      .pipe(catchError(this.errorService.handlerError))
  }
}

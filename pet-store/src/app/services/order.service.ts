import { Injectable } from '@angular/core'
import { Order } from '../models/order/order'
import { PetStoreService } from './pet-store.service'
import { ErrorService } from './errorService'
import { catchError } from 'rxjs/operators'
import { Observable } from 'rxjs'

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
}

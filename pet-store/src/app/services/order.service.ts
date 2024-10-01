import { Injectable } from '@angular/core'
import { Order, OrderContact } from '../models/order/order'
import { PetStoreService } from './pet-store.service'
import { ErrorService } from './errorService'
import { catchError } from 'rxjs/operators'
import { Observable } from 'rxjs'
import { OrderInfo } from '../models/order/order-info'

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private order: Order
  constructor(
    private readonly service: PetStoreService,
    private readonly errorService: ErrorService
  ) {
    this.order = new Order()
    this.order.orderItems = []
  }

  submitOrderContact(contact: OrderContact) {
    this.order = { ...this.order, ...contact }
  }

  /**
   * Creates an order on the backend
   */
  create(): Observable<OrderInfo> {
    return this.service.create(this.order).pipe(catchError(this.errorService.handlerError))
  }

  addToCart(id: number) {
    const orderItem = this.order.orderItems.find(x => x.toyId === id)
    if (orderItem !== undefined) {
      this.order.orderItems.forEach(x => {
        if (x.toyId === orderItem.toyId) {
          x.quantity++
        }
        return x
      })
    } else {
      this.order.orderItems.push({ toyId: id, quantity: 1 })
    }
  }

  removeFromCart(id: number) {
    const orderItem = this.order.orderItems.find(x => x.toyId === id)
    if (orderItem !== undefined) {
      if (orderItem.quantity > 0) {
        this.order.orderItems.forEach(x => {
          if (x.toyId === orderItem.toyId) {
            x.quantity--
          }
          return x
        })
      }
    }
  }

  numberOfItems(id?: number): number {
    if (id !== undefined) {
      const orderItem = this.order.orderItems.find(x => x.toyId === id)
      if (orderItem !== undefined) {
        return orderItem.quantity
      }
      return 0
    }
    return this.order.orderItems.reduce((sum, item) => (sum += item.quantity), 0)
  }

  updateStripeToken(token: string) {
    this.order.tokenId = token
  }

  reinitializeOrderItems() {
    this.order.orderItems = []
  }
}

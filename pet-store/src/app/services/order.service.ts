import { Injectable } from '@angular/core'
import { Order } from '../models/order/order'
import { PetStoreService } from './pet-store.service'
import { ErrorService } from './errorService'
import { catchError } from 'rxjs/operators'

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private readonly order: Order
  constructor(private service: PetStoreService, private errorService: ErrorService) {
    this.order = new Order()
    this.order.orderItems = []
  }

  buy() {
    return this.service.buy(this.order).pipe(catchError(this.errorService.handlerError))
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
}

import { createReducer, on } from '@ngrx/store'

import { Order } from '../../models/order/order'
import { addToCart, createOrderContact, createOrderSuccess, removeFromCart } from './order.actions'

enum OrderStateStatus {
  'draft',
  'loading',
  'created',
  'error',
  'paid',
}

export interface OrderState {
  order: Order
  status: OrderStateStatus
  stripeClientSecret?: string
}

export const initialState: OrderState = {
  order: { orderItems: [] },
  status: OrderStateStatus.draft,
}

export const orderReducer = createReducer(
  initialState,
  on(createOrderSuccess, (_state, order) => ({ order, status: OrderStateStatus.created })),
  on(createOrderContact, (state, orderContact) => ({
    order: { ...state.order, ...orderContact },
    status: OrderStateStatus.draft,
  })),
  on(addToCart, (state, { id }) => {
    const orderItems = state.order.orderItems

    const orderItem = orderItems.find(oi => oi.toyId === id)
    if (orderItem !== undefined) {
      orderItems.forEach(oi => {
        if (oi.toyId === orderItem.toyId) {
          oi.quantity++
        }
        return oi
      })
    } else {
      orderItems.push({ toyId: id, quantity: 1 })
    }
    return { ...state, order: { ...state.order, orderItems } }
  }),
  on(removeFromCart, (state, { id }) => {
    const orderItems = state.order.orderItems

    const orderItem = orderItems.find(oi => oi.toyId === id)
    if (orderItem !== undefined) {
      if (orderItem.quantity > 0) {
        orderItems.forEach(oi => {
          if (oi.toyId === orderItem.toyId) {
            oi.quantity--
          }
          return oi
        })
      } else {
        orderItems.splice(orderItems.indexOf(orderItem), 1)
      }
    }
    return { ...state, order: { ...state.order, orderItems } }
  })
)

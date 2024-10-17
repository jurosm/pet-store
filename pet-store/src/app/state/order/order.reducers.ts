import { createReducer, on } from '@ngrx/store'

import { Order } from '../../models/order/order'
import {
  addToCart,
  createOrderContact,
  createOrderSuccess,
  finishOrder,
  removeFromCart,
  updateStripeToken,
} from './order.actions'

export enum OrderStateStatus {
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
  successfulOrders: Order[]
}

export const initialState: OrderState = {
  order: { orderItem: [] },
  status: OrderStateStatus.draft,
  successfulOrders: [],
}

export const orderReducer = createReducer(
  initialState,
  on(createOrderSuccess, (state, order) => ({ ...state, order, status: OrderStateStatus.created })),
  on(createOrderContact, (state, orderContact) => ({
    ...state,
    order: { ...state.order, ...orderContact },
    status: OrderStateStatus.draft,
  })),
  on(addToCart, (state, { id }) => {
    const orderItems = state.order.orderItem

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
    return { ...state, order: { ...state.order, orderItem: orderItems } }
  }),
  on(removeFromCart, (state, { id }) => {
    const orderItems = state.order.orderItem

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
    return { ...state, order: { ...state.order, orderItem: orderItems } }
  }),
  on(updateStripeToken, (state, { token }) => ({ ...state, stripeClientSecret: token })),
  on(finishOrder, state => ({
    ...initialState,
    order: { orderItem: [] },
    stripeClientSecret: null,
    successfulOrders: [...state.successfulOrders, state.order],
  }))
)

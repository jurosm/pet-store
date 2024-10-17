import { createSelector } from '@ngrx/store'

import { AppState } from '../app.state'
import { OrderState } from './order.reducers'

export const selectOrder = (state: AppState) => state.order

export const numberOfItems = (toyId?: number) =>
  createSelector(selectOrder, (state: OrderState) => {
    if (toyId !== undefined) {
      const orderItem = state.order.orderItem.find(x => x.toyId === toyId)
      if (orderItem !== undefined) {
        return orderItem.quantity
      }
      return 0
    }
    return state.order.orderItem.reduce((sum, item) => (sum += item.quantity), 0)
  })

export const selectFinishedOrder = (orderId: number) =>
  createSelector(selectOrder, (state: OrderState) => {
    const successfulOrder = state.successfulOrders.find(order => order.id === orderId)

    return successfulOrder
  })

import { createSelector } from '@ngrx/store'
import { OrderState } from './order.reducers'
import { AppState } from '../app.state'

export const selectOrder = (state: AppState) => state.order

export const numberOfItems = (toyId?: number) =>
  createSelector(selectOrder, (state: OrderState) => {
    if (toyId !== undefined) {
      const orderItem = state.order.orderItems.find(x => x.toyId === toyId)
      if (orderItem !== undefined) {
        return orderItem.quantity
      }
      return 0
    }
    return state.order.orderItems.reduce((sum, item) => (sum += item.quantity), 0)
  })

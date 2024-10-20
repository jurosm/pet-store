import { createAction, props } from '@ngrx/store'

import { Order, OrderContact } from '../../models/order/order'

export const createOrderContact = createAction(
  '[Order Page] Create Order Contact',
  props<OrderContact>()
)

export const createOrder = createAction('[Order Page] Create Order', props<Order>())

export const createOrderSuccess = createAction('[Order Page] Create Order Success', props<Order>())

export const addToCart = createAction('[Order Page] Add To Cart', props<{ id: number }>())

export const removeFromCart = createAction('[Order Page] Remove From Cart', props<{ id: number }>())

export const createPaymentIntent = createAction('[Order Page] Create Payment Intent')

export const paymentIntentSuccess = createAction('[Order Page] Payment Intent Success')

export const reinitializeOrderItems = createAction('[Order Page] Reinitialize Order Items')

export const updateStripeToken = createAction(
  '[Order Page] Update Stripe Token',
  props<{ token: string }>()
)

export const finishOrder = createAction('[Order Page] Finish order', props<{ id: number }>())

import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { Actions, createEffect, ofType } from '@ngrx/effects'
import { Store } from '@ngrx/store'
import { from, map, switchMap, tap, withLatestFrom } from 'rxjs'

import { OrderService } from '../../services/order.service'
import { AppState } from '../app.state'
import {
  createOrder,
  createOrderSuccess,
  createPaymentIntent,
  finishOrder,
  paymentIntentSuccess,
  updateStripeToken,
} from './order.actions'

@Injectable()
export class OrderEffects {
  constructor(
    private readonly orderService: OrderService,
    private readonly actions$: Actions,
    private readonly store$: Store<AppState>,
    private readonly router: Router
  ) {}

  $createOrder = createEffect(() =>
    this.actions$.pipe(
      ofType(createOrder),
      withLatestFrom(this.store$.select(state => state.order.order)),
      switchMap(([payload, order]) =>
        from(this.orderService.create({ ...order, ...payload })).pipe(
          map(order => {
            this.store$.dispatch(createOrderSuccess(order))
            return createPaymentIntent()
          })
        )
      )
    )
  )

  $createPaymentIntent = createEffect(() =>
    this.actions$.pipe(
      ofType(createPaymentIntent),
      withLatestFrom(this.store$.select(state => state.order.order)),
      switchMap(([_action, order]) =>
        from(this.orderService.createPaymentIntent(order.id)).pipe(
          map(res => {
            this.store$.dispatch(updateStripeToken({ token: res.clientSecret }))
            return paymentIntentSuccess()
          })
        )
      )
    )
  )

  navigateForward$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(paymentIntentSuccess),
        withLatestFrom(this.store$.select(state => state.order.order)),
        map(([_action, order]) => order.id),
        tap(id => this.router.navigateByUrl(`/order/${id}/confirm`))
      ),
    { dispatch: false }
  )

  navigateToComplete$ = createEffect(
    () =>
      this.actions$.pipe(
        ofType(finishOrder),
        map(action => action.id),
        tap(id => this.router.navigateByUrl(`/order/${id}/complete`))
      ),
    { dispatch: false }
  )
}

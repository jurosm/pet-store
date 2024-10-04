import { Injectable } from '@angular/core'
import { Actions, createEffect, ofType } from '@ngrx/effects'
import { Store } from '@ngrx/store'
import { from, map, switchMap, withLatestFrom } from 'rxjs'

import { OrderService } from '../../services/order.service'
import { AppState } from '../app.state'
import { createOrder, createOrderSuccess } from './order.actions'

@Injectable()
export class OrderEffects {
  constructor(
    private readonly orderService: OrderService,
    private readonly actions$: Actions,
    private readonly store$: Store<AppState>
  ) {}

  $createOrder = createEffect(() =>
    this.actions$.pipe(
      ofType(createOrder),
      withLatestFrom(this.store$.select(state => state.order.order)),
      switchMap(([_action, order]) =>
        from(this.orderService.create(order)).pipe(map(order => createOrderSuccess(order)))
      )
    )
  )
}

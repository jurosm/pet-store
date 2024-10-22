import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { Store } from '@ngrx/store'

import { Order } from '../../../models/order/order'
import { AppState } from '../../../state/app.state'
import { selectFinishedOrder } from '../../../state/order/order.selectors'
import { Observable } from 'rxjs'

@Component({
  selector: 'app-order-complete',
  templateUrl: './complete.component.html',
  styleUrl: './complete.component.css',
})
export class CompleteComponent implements OnInit {
  order?: Observable<Order>
  constructor(private readonly store: Store<AppState>, private readonly route: ActivatedRoute) {}
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.order = this.store.select(selectFinishedOrder(+params.get('id')))
    })
  }
}

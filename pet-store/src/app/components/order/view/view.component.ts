import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { Store } from '@ngrx/store'
import { Observable } from 'rxjs'

import { Order } from '../../../models/order/order'
import { AppState } from '../../../state/app.state'
import { selectFinishedOrder } from '../../../state/order/order.selectors'

@Component({
  selector: 'app-order-view',
  templateUrl: './view.component.html',
  styleUrl: './view.component.css',
})
export class ViewComponent implements OnInit {
  constructor(private readonly store: Store<AppState>, private readonly route: ActivatedRoute) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.orderData = this.store.select(selectFinishedOrder(+params.get('id')))
    })
  }
  orderData: Observable<Order>
}

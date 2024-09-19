import { Component } from '@angular/core'
import { PetStoreService } from 'src/app/services/pet-store.service'
import { OrderListItem } from 'src/app/models/order/orderListItem'

@Component({
  selector: 'app-list-orders',
  templateUrl: './list-orders.component.html',
  styleUrls: ['./list-orders.component.css'],
})
export class ListOrdersComponent {
  orderListItems: OrderListItem[]
  constructor(public api: PetStoreService) {
    this.api.getOrders().subscribe(res => {
      this.orderListItems = res
    })
  }
}

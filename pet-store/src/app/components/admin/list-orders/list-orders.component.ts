import { Component } from '@angular/core'
import { OrderListItem } from '../../../../app/models/order/order-list-item'
import { PetStoreService } from '../../../../app/services/pet-store.service'

@Component({
  selector: 'app-list-orders',
  templateUrl: './list-orders.component.html',
  styleUrls: ['./list-orders.component.css'],
})
export class ListOrdersComponent {
  orderListItems: OrderListItem[]
  constructor(private readonly api: PetStoreService) {
    this.api.getOrders().subscribe(res => {
      this.orderListItems = res
    })
  }
}

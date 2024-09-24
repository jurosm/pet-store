import { OrderItem } from './orderItem'

export class OrderContact {
  customerName: string
  customerSurname: string
  country: string
  city: string
  streetAddress: string
}

export class Order extends OrderContact {
  tokenId: string
  orderItems: OrderItem[]
}

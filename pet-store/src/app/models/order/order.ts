import { OrderItem } from './order-item'

export class OrderContact {
  customerName?: string
  customerSurname?: string
  country?: string
  city?: string
  streetAddress?: string
}

export class Order extends OrderContact {
  id?: number
  tokenId?: string
  orderItem: OrderItem[]
  paymentSecret?: string
}

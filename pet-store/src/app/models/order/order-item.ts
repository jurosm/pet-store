import { Toy } from '../toy/toy'

export class OrderItem {
  toyId: number
  quantity: number
  toy?: Toy
}

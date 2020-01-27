import * as Collections from 'typescript-collections';
import { OrderItem } from './orderItem';

export class Order {
  customerName: string;
  customerSurname: string;
  country: string;
  city: string;
  streetAddress: string;
  tokenId: string;
  orderItems: OrderItem[];
}

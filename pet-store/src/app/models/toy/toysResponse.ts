import { ToysUnit } from './toysUnit';
import { Categories } from '../categories/categories';

export class ToysResponse {
  items: ToysUnit[];
  numberOfPages: number;
}

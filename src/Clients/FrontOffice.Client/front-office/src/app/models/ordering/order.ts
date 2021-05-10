import { Guid } from "guid-ts";
import { OrderProduct } from "./order.product";

export class Order {
  id: Guid;
  userId: Guid;
  totalPrice: number;
  createdAt: Date;
  products: Array<OrderProduct>;
}

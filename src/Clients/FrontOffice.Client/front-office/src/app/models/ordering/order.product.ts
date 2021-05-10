import { Guid } from "guid-ts";

export class OrderProduct {
  id: Guid;
  productName: string;
  price: number;
  quantity: number;
}

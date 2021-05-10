import { Guid } from "guid-ts";
import { ShoppingCartItem } from "./shopping.cart.item";

export class ShoppingCart {
  userId: Guid;
  items: Array<ShoppingCartItem>;
  count: number;
  totalPrice: number;
  constructor(userId: Guid, items: Array<ShoppingCartItem>) {
    this.userId = userId;
    this.items = items;
  }
}

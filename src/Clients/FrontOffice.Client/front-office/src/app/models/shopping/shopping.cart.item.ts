import { Guid } from "guid-ts";

export class ShoppingCartItem {
  productId: Guid;
  productName:string;
  price: number;
  quantity: number;

  constructor(productId: Guid,
              productName:string,
              price: number,
              quantity: number) {
    this.productId = productId;
    this.productName = productName;
    this.price = price;
    this.quantity = quantity;
  }

}

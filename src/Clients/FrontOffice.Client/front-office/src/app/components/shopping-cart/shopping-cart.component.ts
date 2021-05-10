import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-ts';
import { ShoppingCart } from 'src/app/models/shopping/shopping.cart';
import { ShoppingCartItem } from 'src/app/models/shopping/shopping.cart.item';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  max: number = 10;
  shoppingCart: ShoppingCart =  null;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketService.shoppingCart$.subscribe(data => {
      this.shoppingCart =  data;
    });
  }

  onHandlePlus(cartItem: ShoppingCartItem) {

    cartItem.quantity = cartItem.quantity + 1;

    if(cartItem.quantity > this.max)
      cartItem.quantity = this.max;

     this.basketService.onCreateOrUpdateShoppingCart(cartItem).subscribe(data => {
      this.shoppingCart =  data;
    });
  }

  onHandleMinus(cartItem: ShoppingCartItem) {
    cartItem.quantity = cartItem.quantity - 1;
    if(cartItem.quantity < 1)
      cartItem.quantity = 1;
      this.basketService.onCreateOrUpdateShoppingCart(cartItem).subscribe(data => {
        this.shoppingCart =  data;
      });
  }

  onHandleRemove(productId: Guid) {
    this.basketService.onDeleteShoppingCartItem$(productId).subscribe(data => {
      this.shoppingCart = data;
      this.basketService.setCount(data.count);
    });
  }
}

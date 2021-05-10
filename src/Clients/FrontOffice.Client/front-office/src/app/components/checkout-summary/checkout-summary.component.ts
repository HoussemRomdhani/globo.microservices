import { Component, OnInit } from '@angular/core';
import { ShoppingCart } from 'src/app/models/shopping/shopping.cart';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-checkout-summary',
  templateUrl: './checkout-summary.component.html',
  styleUrls: ['./checkout-summary.component.css']
})
export class CheckoutSummaryComponent implements OnInit {

  shoppingCart: ShoppingCart =  null;
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.basketService.shoppingCart$.subscribe(data => {
      this.shoppingCart =  data;
    });
  }

}

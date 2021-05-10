import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ProductModel } from 'src/app/models/product.model';
import { ShoppingCartItem } from 'src/app/models/shopping/shopping.cart.item';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-purchase',
  templateUrl: './purchase.component.html',
  styleUrls: ['./purchase.component.css']
})
export class PurchaseComponent {

  @Input() product: ProductModel;
  items = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(item => { return {key: item.toString(), value:  item.toString()} });
  purchaseForm = new FormGroup({
    count: new FormControl(this.items[0]),
  });

  constructor(private basketService: BasketService) {
  }

  onSubmit() {
    const item = new ShoppingCartItem(this.product.id,
       this.product.name,
       this.product.price,
       + this.purchaseForm.value.count.key
      );

      this.basketService.onCreateOrUpdateShoppingCart(item).subscribe(data => {
        this.basketService.setCount(data.count);
      });
  }
}

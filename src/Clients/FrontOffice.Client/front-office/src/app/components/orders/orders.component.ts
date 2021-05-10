import { Component, OnInit } from '@angular/core';
import { Order } from 'src/app/models/ordering/order';
import { OrderingService } from 'src/app/services/ordering.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css']
})
export class OrdersComponent implements OnInit {

  orders: Array<Order> = [];
  constructor(private oreringService: OrderingService) {
  }

  ngOnInit(): void {
     this.oreringService.orders$.subscribe(data => {
      this.orders =  data;
    });
  }
}

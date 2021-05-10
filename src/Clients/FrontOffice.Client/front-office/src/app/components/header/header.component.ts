import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { BasketService } from 'src/app/services/basket.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

   isAuthenticated: Boolean = false;
   name : string = "";
   count: number = 0;
  constructor(private authService: AuthService, public basketService: BasketService){
    this.basketService.shoppingCart$.subscribe(data => {
      this.basketService.setCount(data.count);
    });
   }

  ngOnInit(): void {
  this.authService.getUser().then(user => {
    if (user) {
      this.isAuthenticated = true;
      this.name = user.profile.name;
    } else {
      this.name = '';
      this.isAuthenticated = false;
    }
  }).catch(err => {
    console.log(err)
  });

  this.basketService.getCount().subscribe(data => {
    this.count = data;
  });
}

  public onLogin() {
    this.authService.login().catch(err => {
      console.log(err);
    });
  }

  public onLogout() {
    this.authService.logout().catch(err => {
      console.log(err);
    });
  }
}

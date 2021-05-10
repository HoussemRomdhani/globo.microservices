import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';
import { ShoppingCart } from '../models/shopping/shopping.cart';
import { ShoppingCartItem } from '../models/shopping/shopping.cart.item';
import { Guid } from 'guid-ts';
import { concatMap, count, map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { BasketCheckoutForm } from '../models/basket.checkout.form';
import { BasketCheckout } from '../models/basket.checkout';

@Injectable({
  providedIn: 'root',
})
export class BasketService {


  count : BehaviorSubject<number> = new BehaviorSubject<number>(0);
  constructor(private httpClient: HttpClient, private authService: AuthService) {
  }

  user$ = this.authService.user$();

  shoppingCart$ = this.user$.pipe(
      concatMap(user => {
      return this.httpClient.get<ShoppingCart>(`${environment.basketRoot}/basket?userId=${user.profile.sub}`,
                                                { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
  }));

  setCount = (count: Number) => this.count.next(count as number);
  getCount = () =>  this.count.asObservable();

  onCreateOrUpdateShoppingCart(cartItem: ShoppingCartItem): Observable<ShoppingCart> {
    return this.shoppingCart$.pipe(
        concatMap(shoppingCart => {
              if(!shoppingCart || shoppingCart == null || shoppingCart.items.length == 0)
                return this.onCreateShoppingCart$(cartItem);
                return this.onUpdateShoppingCartItem$(cartItem);
           }));
   }

   onUpdateShoppingCartItem$(item: ShoppingCartItem): Observable<ShoppingCart> {
    return this.user$.pipe(
      concatMap(user => {
      return this.httpClient.put<ShoppingCart>(`${environment.basketRoot}/basket?userId=${user.profile.sub}`,
                                                item,
                                                { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
    }));
   }

   onCreateShoppingCart$(item: ShoppingCartItem): Observable<ShoppingCart> {
    return this.user$.pipe(
      concatMap(user => {

      const body = new ShoppingCart(user.profile.sub as unknown as Guid,
                                    new Array<ShoppingCartItem>(item));

     return this.httpClient.post<ShoppingCart>(`${environment.basketRoot}/basket?userId=${user.profile.sub}`,
                                                 body,
                                                { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
    }));
   }


   onDeleteShoppingCartItem$(itemId: Guid): Observable<ShoppingCart> {
    return this.user$.pipe(
      concatMap(user => {
      return this.httpClient.delete<ShoppingCart>(`${environment.basketRoot}/basket?userId=${user.profile.sub}&itemId=${itemId}`,
                                                   { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
    }));
   }

   onBasketCheckout(basketCheckoutForm : BasketCheckoutForm): Observable<any> {
    return this.user$.pipe(
      concatMap(user => {

       const body = new BasketCheckout(user.profile.sub as unknown as Guid, basketCheckoutForm);

      return this.httpClient.post<ShoppingCart>(`${environment.basketRoot}/basket/checkout`,
                                                body,
                                                { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
      }));
   }

}

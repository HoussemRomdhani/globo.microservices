import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { concatMap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Order } from '../models/ordering/order';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class OrderingService {

  constructor(private httpClient: HttpClient, private authService: AuthService) {
  }

  user$ = this.authService.user$();

  orders$ = this.user$.pipe(
    concatMap(user => {
    return this.httpClient.get<Array<Order>>(`${environment.orderingRoot}/orders?userId=${user.profile.sub}`,
                                              { headers: new HttpHeaders({ 'Authorization': `Bearer ${user.access_token}` })});
  }));

}

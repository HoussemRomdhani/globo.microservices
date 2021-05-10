import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EventModel } from '../models/event.model';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';
import { Category } from '../models/Category.model';
import { ProductModel } from '../models/product.model';
import { Guid } from 'guid-ts';

@Injectable({
  providedIn: 'root',
})
export class EventCatalogService {

  constructor(private httpClient: HttpClient, private authService: AuthService) {

  }

  async getCategories(): Promise<Category[]> {
    const value = await this.getToken();
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${value}` });
    const resp = await this.httpClient.get<Category[]>(`${environment.apiRoot}/categories`, { headers: headers })
      .toPromise();
    return resp;
  }

  // async getEvents(): Promise<EventModel[]> {
  //   const value = await this.getToken();
  //   const headers = new HttpHeaders({ 'Authorization': `Bearer ${value}` });
  //   const resp = await this.httpClient.get<EventModel[]>(`${environment.apiRoot}/events`, { headers: headers })
  //     .toPromise();
  //   return resp;
  // }

  async getProducts(): Promise<ProductModel[]> {
    const value = await this.getToken();
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${value}` });
    const resp = await this.httpClient.get<ProductModel[]>(`${environment.apiRoot}/products`, { headers: headers })
      .toPromise();
    return resp;
  }

  async getProduct(id: Guid): Promise<ProductModel> {
    const value = await this.getToken();
    const headers = new HttpHeaders({ 'Authorization': `Bearer ${value}` });
    const resp = await this.httpClient.get<ProductModel>(`${environment.apiRoot}/products/${id}`, { headers: headers })
      .toPromise();
    return resp;
  }

 private getToken():Promise<string> {
  return this.authService.getUser().then(user => {
     if(user){
        return user.access_token;
     }else  {
       return '';
     }
  });
 }

}

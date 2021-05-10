import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable} from 'rxjs';
import {  map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Country } from '../models/country.model';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {

  constructor(private httpClient: HttpClient) { }

   countries$: Observable<Array<Country>> = this.httpClient.get<any>(`${environment.countiesApiRoot}/countries`)
                               .pipe(
                                  map(response => response.data.map(item => new Country(item.country, item.cities)),
                                 ));

//  getCities = (name: string) : Observable<Array<string>> => {
//   return this.httpClient.get<Array<Country>>(`${environment.countiesApiRoot}/countries`)
//                                .pipe(
//                                   map(response => response.data.find((x: Country)  => x.country == name).cities)
//                                  );
//  }

}



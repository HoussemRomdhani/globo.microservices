export class Country {
  country: string;
  cities: Array<string>;
  constructor(country: string, cities: Array<string>){
    this.country = country;
    this.cities = cities;
  }
}

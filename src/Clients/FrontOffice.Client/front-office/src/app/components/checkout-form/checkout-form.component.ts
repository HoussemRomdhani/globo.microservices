import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import {  Router } from '@angular/router';
import { BasketCheckoutForm } from 'src/app/models/basket.checkout.form';
import { Country } from 'src/app/models/country.model';
import { AuthService } from 'src/app/services/auth.service';
import { BasketService } from 'src/app/services/basket.service';
import { CountriesService } from 'src/app/services/countries.service';

@Component({
  selector: 'app-checkout-form',
  templateUrl: './checkout-form.component.html',
  styleUrls: ['./checkout-form.component.css']
})
export class CheckoutFormComponent implements OnInit {

  countries: Array<Country> = [];
  cities: Array<string> = [];

  checkoutForm: FormGroup = null;
  get controlsForm  () { return this.checkoutForm.controls}  ;
  get controlsAddress  () { return  (this.checkoutForm.controls['address'] as FormGroup).controls } ;
  get controlsPayment  () { return  (this.checkoutForm.controls['payment'] as FormGroup).controls } ;
  paymentMethods = ["credit", "debit", "paypal"];
  submitted = false;

  constructor(private authService: AuthService,
              private countriesService: CountriesService,
              private fb: FormBuilder,
              private basketService: BasketService,
              private router: Router) {
  }

  ngOnInit(): void {

    this.checkoutForm = this.fb.group({
      firstName: new FormControl('', [Validators.required]),
      lastName: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      address: this.fb.group({
        street: new FormControl('', [Validators.required]),
        city: new FormControl('', [Validators.required]),
        country: new FormControl('', [Validators.required]),
        zip: new FormControl('', [Validators.required])
      }),
      payment: this.fb.group({
        cardName: new FormControl('', [Validators.required]),
        cardNumber: new FormControl('', [Validators.required]),
        expiration: new FormControl('', [Validators.required]),
        cvv: new FormControl('', [Validators.required]),
        paymentMethod: new FormControl('credit', [Validators.required]),
      }),
    });

    this.countriesService.countries$.subscribe(data => {
      this.countries = data;
      this.cities = data.find(x => x.country == this.controlsAddress.country.value).cities;
    });

    this.authService.user$().subscribe(data => {

      this.controlsForm.firstName.setValue(data.profile.given_name);
      this.controlsForm.lastName.setValue(data.profile.family_name);
      this.controlsForm.email.setValue(data.profile.email);

      this.controlsAddress.street.setValue(data.profile.address?.street_address);
      this.controlsAddress.country.setValue(data.profile.address?.country);
      this.controlsAddress.city.setValue(data.profile.address?.locality);
      this.controlsAddress.zip.setValue(data.profile.address?.postal_code);
      this.controlsPayment.cardName.setValue('');

    });

    this.controlsAddress.country.valueChanges
                     .subscribe(val =>
        {
          this.cities = this.countries.find(x => x.country == val).cities;
       });
  }

  onSubmit() {
    this.submitted = true;

    if (this.checkoutForm.invalid || this.controlsAddress.invalid
                                  || this.controlsPayment.invalid ) {
       return;
     }

     this.basketService.onBasketCheckout(this.checkoutForm.value as BasketCheckoutForm)
                       .subscribe(() => {
                         this.router.navigate(['/orders']);
                       })
}
}

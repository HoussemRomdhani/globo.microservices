import { Guid } from "guid-ts";
import { BasketCheckoutForm } from "./basket.checkout.form";

export class BasketCheckout {
  userId: Guid;
  totalPrice: number;

   firstName: string;
   lastName: string;
   emailAddress: string;

   addressLine: string;
   country: string;
   state: string;
   zipCode: string;

   cardName: string;
   cardNumber: string;
   expiration: string;
   cVV: string;
   paymentMethod: string;

   constructor(userId: Guid, basketCheckoutForm: BasketCheckoutForm) {
      this.userId = userId;

      this.totalPrice = 0;

      this.firstName = basketCheckoutForm.firstName;
      this.lastName = basketCheckoutForm.lastName;
      this.emailAddress = basketCheckoutForm.email;

      this.addressLine = basketCheckoutForm.address.street;
      this.country = basketCheckoutForm.address.country;
      this.state = basketCheckoutForm.address.city;
      this.zipCode = basketCheckoutForm.address.zip;

      this.cardName = basketCheckoutForm.payment.cardName;
      this.cardNumber = basketCheckoutForm.payment.cardNumber;
      this.expiration = basketCheckoutForm.payment.expiration;
      this.cVV = basketCheckoutForm.payment.cvv;
      this.paymentMethod = basketCheckoutForm.payment.paymentMethod;
   }

}

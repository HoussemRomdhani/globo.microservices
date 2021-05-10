export class Address {
  street: string;
  city: string;
  country: string;
  zip: string;
}

export class Payment {
  cardName: string;
  cardNumber: string;
  expiration: string;
  cvv: string;
  paymentMethod: string;
}

export class BasketCheckoutForm {
   firstName: string;
   lastName: string;
   email: string;
   address: Address;
   payment: Payment;
}


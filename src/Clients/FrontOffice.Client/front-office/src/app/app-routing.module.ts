import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './components/auth-callback/auth-callback.component';
import { CatalogComponent } from './components/catalog/catalog.component';
import { ProductComponent } from './components/catalog/product/product.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { OrdersComponent } from './components/orders/orders.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ShoppingCartComponent } from './components/shopping-cart/shopping-cart.component';
import { AuthService } from './services/auth.service';


const routes: Routes = [
            { path: '', redirectTo: 'catalog', pathMatch: 'full' },
            { path: 'catalog', component: CatalogComponent },
            { path: 'catalog/:id', component: ProductComponent },
            { path: 'cart', component: ShoppingCartComponent },
            { path: 'orders', component: OrdersComponent },
            { path: 'profile', component: ProfileComponent },
            { path: 'checkout', component: CheckoutComponent },
            {path: 'auth-callback', component: AuthCallbackComponent},
            { path: '**', redirectTo: 'catalog' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [ AuthService]
})
export class AppRoutingModule { }

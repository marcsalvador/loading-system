import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MenuComponent } from './pages/menu/menu.component';
import { BuyCardComponent } from './pages/buy-card/buy-card.component';
import { LoadCardComponent } from './pages/load-card/load-card.component';
import { DiscountRegistrationComponent } from './pages/discount-registration/discount-registration.component';
import { UseCardComponent } from './pages/use-card/use-card.component';
import { SuccessfulComponent } from './pages/successful/successful.component';

const routes: Routes = [
  { path: '', component: MenuComponent },
  { path: 'buy-card', component: BuyCardComponent },
  { path: 'load-card', component: LoadCardComponent },
  { path: 'discount-registration', component: DiscountRegistrationComponent },
  { path: 'use-card', component: UseCardComponent },
  { path: 'successful', component: SuccessfulComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './pages/menu/menu.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from '@angular/http';
import { BuyCardComponent } from './pages/buy-card/buy-card.component';
import { LoadCardComponent } from './pages/load-card/load-card.component';
import { DiscountRegistrationComponent } from './pages/discount-registration/discount-registration.component';
import { UseCardComponent } from './pages/use-card/use-card.component';

import { CustomerService } from './api/customer.service';
import { SuccessfulComponent } from './pages/successful/successful.component';
import { DatePipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    BuyCardComponent,
    LoadCardComponent,
    DiscountRegistrationComponent,
    UseCardComponent,
    SuccessfulComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatSnackBarModule,
    NgxSpinnerModule
  ],
  providers: [
    CustomerService,
    DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { OrderComponent } from './components/order/order.component';

import { Module as StripeModule } from 'stripe-angular';
import { OrderService } from './services/order.service';
import { PetStoreService } from './services/pet-store.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    OrderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StripeModule.forRoot()
  ],
  providers: [OrderService, PetStoreService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { Component, OnInit } from '@angular/core';
import { StripeScriptTag, StripeToken, StripeSource, StripeCard, Stripe  } from 'stripe-angular';
import { OrderService } from 'src/app/services/order.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Order } from 'src/app/models/order/order';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  private publishableKey = 'pk_test_bcChbYVcDBLs1fyRed6W76tf003ga0Kj98';
  invalidError: any;
  orderForm: FormGroup;
  order: Order;

  constructor(public StripeScriptTag: StripeScriptTag, public orderService: OrderService) {
    this.StripeScriptTag.setPublishableKey(this.publishableKey);
    this.order = new Order();
    this.orderForm = new FormGroup({
      customerName: new FormControl(Validators.required),
      customerSurname: new FormControl(Validators.required),
      country: new FormControl(Validators.required),
      city: new FormControl(Validators.required),
      streetAddress: new FormControl(Validators.required)
    });
  }


  getToken(){

  }

  ngOnInit() {
  }

  onStripeInvalid( error: Error ) {
    console.log('Validation Error', error);
  }

  setStripeToken( token: StripeToken ) {
    console.log('Stripe token', token);
    this.orderService.order.tokenId = token.id;
    this.orderService.buy();
  }

  setStripeSource( source: StripeSource ) {
    console.log('Stripe source', source);
  }

  onStripeError( error: Error ) {
    console.error('Stripe error', error);
  }

}

import { Component, OnInit } from '@angular/core';
import { StripeScriptTag, StripeToken, StripeSource, StripeCard, Stripe  } from 'stripe-angular';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent implements OnInit {
  private publishableKey = 'pk_test_bcChbYVcDBLs1fyRed6W76tf003ga0Kj98';
  invalidError:any;

  constructor(public StripeScriptTag: StripeScriptTag) {
    this.StripeScriptTag.setPublishableKey(this.publishableKey);
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
  }

  setStripeSource( source: StripeSource ) {
    console.log('Stripe source', source);
  }

  onStripeError( error: Error ) {
    console.error('Stripe error', error);
  }

}

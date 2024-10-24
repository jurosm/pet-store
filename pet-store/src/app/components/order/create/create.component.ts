import { Component } from '@angular/core'
import { FormGroup, FormControl } from '@angular/forms'
import { Store } from '@ngrx/store'
import * as Collections from 'typescript-collections'

import { AppState } from '../../../state/app.state'
import { createOrder } from '../../../state/order/order.actions'

@Component({
  selector: 'app-order-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent {
  createOrder() {
    this.store.dispatch(createOrder(this.orderForm.value))
  }
  invalidError: any
  orderForm: FormGroup
  isSuccessful: boolean
  transactionIsNotSuccessful: boolean
  validationServerError: Collections.Dictionary<string, string>
  serverErrorMessage: string

  constructor(
    // private readonly router: Router,
    private readonly store: Store<AppState>
  ) {
    this.transactionIsNotSuccessful = false
    this.isSuccessful = false
    this.orderForm = new FormGroup({
      customerName: new FormControl(),
      customerSurname: new FormControl(),
      country: new FormControl(),
      city: new FormControl(),
      streetAddress: new FormControl(),
    })
    this.validationServerError = new Collections.Dictionary<string, string>()
  }

  onStripeInvalid(error: Error) {
    console.log('Validation Error', error)
  }

  onStripeError(error: Error) {
    console.error('Stripe error', error)
  }
}

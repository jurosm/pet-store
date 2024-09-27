import { Component } from '@angular/core'
import { OrderService } from 'src/app/services/order.service'
import { FormGroup, FormControl, Validators } from '@angular/forms'
import { Order } from 'src/app/models/order/order'
// import { CollectionConverter } from 'src/app/helper/collectionConverter'
// import { ErrorResponse } from 'src/app/models/error/errorResponse'
import * as Collections from 'typescript-collections'

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent {
  invalidError: any
  orderForm: FormGroup
  order: Order
  isSuccessful: boolean
  transactionIsNotSuccessful: boolean
  validationServerError: Collections.Dictionary<string, string>
  serverErrorMessage: string

  constructor( public orderService: OrderService) {
    this.transactionIsNotSuccessful = false
    this.isSuccessful = false
    this.order = new Order()
    this.orderForm = new FormGroup({
      customerName: new FormControl(Validators.required),
      customerSurname: new FormControl(Validators.required),
      country: new FormControl(Validators.required),
      city: new FormControl(Validators.required),
      streetAddress: new FormControl(Validators.required),
    })
    this.validationServerError = new Collections.Dictionary<string, string>()
  }

  onStripeInvalid(error: Error) {
    console.log('Validation Error', error)
  }

  /**
   * Updates the token on the order and creates the order on the backend
   * @param token Token returned from Stripe
   */
  // setStripeToken(token: stripe.Token) {
  //   console.log('Set Stripe token', token)
  //   this.orderService.updateStripeToken(token.id)
  //   this.orderService.create().subscribe({
  //     next: _res => {
  //       this.isSuccessful = true
  //       this.transactionIsNotSuccessful = false
  //       this.orderService.reinitializeOrderItems()
  //     },
  //     error: err => {
  //       this.transactionIsNotSuccessful = true
  //       this.isSuccessful = false
  //       if (err instanceof ErrorResponse) {
  //         this.handleError(err)
  //       }
  //     },
  //   })
  // }

  // setStripeSource(source: StripeSource) {
  //   console.log('Set Stripe source', source)
  // }

  onStripeError(error: Error) {
    console.error('Stripe error', error)
  }

  // private handleError(err) {
  //   if (err instanceof ErrorResponse) {
  //     this.transactionIsNotSuccessful = true
  //     this.isSuccessful = false
  //     this.validationServerError = CollectionConverter.errorArrayToDictionary(err.errors)
  //     this.serverErrorMessage = err.message
  //   }
  // }
}

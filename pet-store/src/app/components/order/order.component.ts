import { Component } from '@angular/core'
import { FormGroup, FormControl } from '@angular/forms'
import { Router } from '@angular/router'
import { OrderService } from 'src/app/services/order.service'
import * as Collections from 'typescript-collections'

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css'],
})
export class OrderComponent {
  createOrder() {
    this.orderService.submitOrderContact(this.orderForm.value)
    this.orderService.create().subscribe({
      next: (res) => {
        this.isSuccessful = true
        this.router.navigateByUrl(`/order/${res.order.id}/confirm`)
      },
    })
  }
  invalidError: any
  orderForm: FormGroup
  isSuccessful: boolean
  transactionIsNotSuccessful: boolean
  validationServerError: Collections.Dictionary<string, string>
  serverErrorMessage: string

  constructor(private readonly orderService: OrderService, private readonly router: Router) {
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

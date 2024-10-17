import { Component, OnInit } from '@angular/core'
import { Store } from '@ngrx/store'
import { loadStripe, Stripe } from '@stripe/stripe-js'

import { environment } from '../../../environments/environment'
import { AppState } from '../../state/app.state'
import { finishOrder } from '../../state/order/order.actions'

@Component({
  selector: 'app-order-confirm',
  templateUrl: './confirm.component.html',
  styleUrl: './confirm.component.css',
})
export class ConfirmComponent implements OnInit {
  private clientSecret = ''
  private readonly publishableKey = environment.stripeKey
  private readonly returnBaseUrl = environment.stripeReturnBaseUrl
  private stripe: Stripe

  constructor(private readonly store: Store<AppState>) {}

  async ngOnInit(): Promise<void> {
    this.store
      .select(state => state.order)
      .subscribe(async res => {
        this.clientSecret = res.stripeClientSecret

        this.stripe = await loadStripe(this.publishableKey)

        // Initialize Stripe Elements with the PaymentIntent's clientSecret,
        // then mount the payment element.
        const loader = 'auto'
        const elements = this.stripe.elements({ clientSecret: this.clientSecret, loader })
        const paymentElement = elements.create('payment')
        paymentElement.mount('#payment-element')

        // When the form is submitted...
        const form = document.getElementById('payment-form')
        let submitted = false
        form.addEventListener('submit', async e => {
          e.preventDefault()

          // Disable double submission of the form
          if (submitted) {
            return
          }
          submitted = true
          form.querySelector('button').disabled = true

          // Confirm the payment given the clientSecret
          // from the payment intent that was just created on
          // the server.
          const { error: stripeError } = await this.stripe.confirmPayment({
            elements,
            confirmParams: {
              return_url: `${this.returnBaseUrl}/order/${res.order.id}/complete`,
            },
            redirect: 'if_required',
          })

          if (stripeError) {
            console.error(stripeError.message)
            // reenable the form.
            submitted = false
            form.querySelector('button').disabled = false
            return
          } else {
            this.store.dispatch(finishOrder({ id: res.order.id }))
          }
        })
      })
  }
}

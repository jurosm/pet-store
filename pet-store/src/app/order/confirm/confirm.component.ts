import { Component, OnInit } from '@angular/core'
import { loadStripe, Stripe } from '@stripe/stripe-js'
import { environment } from '../../../environments/environment'

@Component({
  selector: 'app-order-confirm',
  templateUrl: './confirm.component.html',
  styleUrl: './confirm.component.css',
})
export class ConfirmComponent implements OnInit {
  private readonly clientSecret = ''
  private readonly publishableKey = environment.stripeKey
  private stripe: Stripe

  async ngOnInit(): Promise<void> {
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
          return_url: `http://localhost:4200/order/complete`,
        },
      })

      if (stripeError) {
        console.error(stripeError.message)
        // reenable the form.
        submitted = false
        form.querySelector('button').disabled = false
        return
      }
    })
  }
}

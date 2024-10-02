import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'
import { CreateComponent } from './create/create.component'
import { CompleteComponent } from './complete/complete.component'
import { ConfirmComponent } from './confirm/confirm.component'
import { OrderRoutingModule } from './order-routing.module'
import { ReactiveFormsModule } from '@angular/forms'

@NgModule({
  declarations: [CreateComponent, CompleteComponent, ConfirmComponent],
  exports: [CreateComponent, CompleteComponent, ConfirmComponent],
  imports: [CommonModule, OrderRoutingModule, ReactiveFormsModule],
})
export class OrderModule {}

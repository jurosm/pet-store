import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'
import { ConfirmComponent } from './confirm/confirm.component'
import { CompleteComponent } from './complete/complete.component'
import { CreateComponent } from './create/create.component'

const routes = [
    { path: 'order/:id/confirm', component: ConfirmComponent },
    { path: 'order/:id/complete', component: CompleteComponent },
    { path: 'order', component: CreateComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class OrderRoutingModule {}

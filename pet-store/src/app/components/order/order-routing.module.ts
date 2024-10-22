import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'

import { CompleteComponent } from './complete/complete.component'
import { ConfirmComponent } from './confirm/confirm.component'
import { CreateComponent } from './create/create.component'
import { ViewComponent } from './view/view.component'

const routes = [
  { path: 'order/:id/confirm', component: ConfirmComponent },
  { path: 'order/:id/complete', component: CompleteComponent },
  { path: 'order', component: CreateComponent },
  { path: 'order/:id', component: ViewComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class OrderRoutingModule {}

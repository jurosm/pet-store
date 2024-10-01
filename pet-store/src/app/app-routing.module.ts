import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'
import { OrderComponent } from './components/order/order.component'
import { LoginComponent } from './components/auth/login/login.component'
import { ToyComponent } from './components/toy/toy.component'
import { ToysComponent } from './components/toy/toys/toys.component'
import { ErrorComponent } from './components/error/error.component'
import { CreateToyComponent } from './components/admin/create-toy/create-toy.component'
import { ListOrdersComponent } from './components/admin/list-orders/list-orders.component'
import { CategoryComponent } from './components/category/category.component'
import { RouteGuard } from './guards/route.guard'
import { OrderConfirmComponent } from './components/order/order-confirm/order-confirm.component'
import { OrderCompleteComponent } from './components/order/order-complete/order-complete.component'

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'order', component: OrderComponent },
  { path: '', component: ToysComponent },
  { path: 'toys/:id', component: ToyComponent },
  { path: 'error', component: ErrorComponent },
  { path: 'toy/create', component: CreateToyComponent, canActivate: [RouteGuard] },
  { path: 'toy/edit/:id', component: CreateToyComponent, canActivate: [RouteGuard] },
  { path: 'orders', component: ListOrdersComponent, canActivate: [RouteGuard] },
  { path: 'category', component: CategoryComponent, canActivate: [RouteGuard] },
  { path: 'order/:id/confirm', component: OrderConfirmComponent },
  { path: 'order/:id/complete', component: OrderCompleteComponent },
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

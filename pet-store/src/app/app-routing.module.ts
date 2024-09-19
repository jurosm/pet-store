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
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}

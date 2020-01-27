import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderComponent } from './components/order/order.component';
import { LoginComponent } from './components/auth/login/login.component';
import { ToyComponent } from './components/toy/toy.component';
import { ToysComponent } from './components/toy/toys/toys.component';
import { ErrorComponent } from './components/error/error.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'order', component: OrderComponent },
  { path: '', component: ToysComponent },
  { path: 'toys/:id', component: ToyComponent },
  { path: 'error', component: ErrorComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

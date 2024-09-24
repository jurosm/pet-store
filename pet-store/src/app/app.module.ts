import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { JwtHelperService, JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt'
import { TokenInterceptor } from './interceptors/tokenInterceptor'
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { HeaderComponent } from './components/shared/header/header.component'
import { FooterComponent } from './components/shared/footer/footer.component'
import { OrderComponent } from './components/order/order.component'

import { OrderService } from './services/order.service'
import { PetStoreService } from './services/pet-store.service'
import { HTTP_INTERCEPTORS, provideHttpClient } from '@angular/common/http'
import { ToysComponent } from './components/toy/toys/toys.component'
import { ToyComponent } from './components/toy/toy.component'
import { AuthService } from './services/auth.service'
import { RouteGuard } from './guards/route.guard'
import { LoginComponent } from './components/auth/login/login.component'
import { ReactiveFormsModule, FormsModule } from '@angular/forms'
import { RouterModule } from '@angular/router'
import { ErrorComponent } from './components/error/error.component'
import { CreateToyComponent } from './components/admin/create-toy/create-toy.component'
import { ListOrdersComponent } from './components/admin/list-orders/list-orders.component'
import { CommentsComponent } from './components/toy/comments/comments.component'
import { CategoryComponent } from './components/category/category.component'
import { OrderConfirmComponent } from './components/order/order-confirm/order-confirm.component'
import { OrderCompleteComponent } from './components/order/order-complete/order-complete.component'

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    OrderConfirmComponent,
    OrderComponent,
    ToysComponent,
    ToyComponent,
    LoginComponent,
    ErrorComponent,
    CreateToyComponent,
    ListOrdersComponent,
    CommentsComponent,
    CategoryComponent,
    OrderCompleteComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule,
    FontAwesomeModule,
    AppRoutingModule,
    JwtModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService,
    OrderService,
    PetStoreService,
    AuthService,
    RouteGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    provideHttpClient(),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { JwtHelperService, JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { TokenInterceptor } from './interceptors/tokenInterceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { OrderComponent } from './components/order/order.component';

import { Module as StripeModule } from 'stripe-angular';
import { OrderService } from './services/order.service';
import { PetStoreService } from './services/pet-store.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ToysComponent } from './components/toy/toys/toys.component';
import { ToyComponent } from './components/toy/toy.component';
import { AuthService } from './services/auth.service';
import { RouteGuard } from './guards/route.guard';
import { LoginComponent } from './components/auth/login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ErrorComponent } from './components/error/error.component';
import { CreateToyComponent } from './components/admin/create-toy/create-toy.component';
import { ListOrdersComponent } from './components/admin/list-orders/list-orders.component';
import { CommentsComponent } from './components/toy/comments/comments.component';
import { CategoryComponent } from './components/category/category.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    OrderComponent,
    ToysComponent,
    ToyComponent,
    LoginComponent,
    ErrorComponent,
    CreateToyComponent,
    ListOrdersComponent,
    CommentsComponent,
    CategoryComponent
  ],
  imports: [
    BrowserModule,
    RouterModule,
    FontAwesomeModule,
    AppRoutingModule,
    HttpClientModule,
    StripeModule.forRoot(),
    JwtModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: JWT_OPTIONS, useValue: JWT_OPTIONS }, JwtHelperService, OrderService, PetStoreService, AuthService, RouteGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }

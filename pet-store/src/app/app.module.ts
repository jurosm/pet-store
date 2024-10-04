import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { BrowserModule } from '@angular/platform-browser'
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap'
import { NgModule } from '@angular/core'
import { JwtHelperService, JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt'
import { TokenInterceptor } from './interceptors/tokenInterceptor'
import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { HeaderComponent } from './components/shared/header/header.component'
import { FooterComponent } from './components/shared/footer/footer.component'

import { OrderService } from './services/order.service'
import { PetStoreService } from './services/pet-store.service'
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors } from '@angular/common/http'
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
import { authInterceptor } from './helper/authInterceptor'
import { OrderModule } from './order/order.module'
import { ActionReducer, MetaReducer, StoreModule } from '@ngrx/store'
import { EffectsModule } from '@ngrx/effects'
import { orderReducer } from './state/order/order.reducers'
import { OrderEffects } from './state/order/order.effects'
import { localStorageSync } from 'ngrx-store-localstorage'

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return localStorageSync({ keys: ['order'], rehydrate: true })(reducer)
}
const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer]

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    ToysComponent,
    ToyComponent,
    LoginComponent,
    ErrorComponent,
    CreateToyComponent,
    ListOrdersComponent,
    CommentsComponent,
    CategoryComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule,
    FontAwesomeModule,
    AppRoutingModule,
    JwtModule,
    FormsModule,
    ReactiveFormsModule,
    NgbPaginationModule,
    OrderModule,
    StoreModule.forRoot({ order: orderReducer }, { metaReducers }),
    EffectsModule.forRoot([OrderEffects]),
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
    provideHttpClient(withInterceptors([authInterceptor])),
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}

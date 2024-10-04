import { Component, OnInit } from '@angular/core'
import { AuthService } from '../../../../app/services/auth.service'
import { Router } from '@angular/router'
import { select, Store } from '@ngrx/store'
import { numberOfItems } from '../../../state/order/order.selectors'
import { Observable } from 'rxjs'
import { AppState } from '../../../state/app.state'

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  navbarOpen = false

  numberOfItemsInCart: Observable<number>

  toggleNavbar(): void {
    this.navbarOpen = !this.navbarOpen
  }

  constructor(
    protected readonly authService: AuthService,
    private readonly store: Store<AppState>,
    protected readonly router: Router
  ) {}
  ngOnInit(): void {
    this.numberOfItemsInCart = this.store.pipe(select(numberOfItems()))
  }
}

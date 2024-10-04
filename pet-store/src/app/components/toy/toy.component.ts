import { Component, OnInit } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { OrderService } from 'src/app/services/order.service'
import { Toy } from 'src/app/models/toy/toy'
import { PetStoreService } from 'src/app/services/pet-store.service'
import { AuthService } from 'src/app/services/auth.service'
import { select, Store } from '@ngrx/store'
import { AppState } from '../../state/app.state'
import { numberOfItems } from '../../state/order/order.selectors'
import { map, Observable } from 'rxjs'
import { addToCart, removeFromCart } from '../../state/order/order.actions'

@Component({
  selector: 'app-toy',
  templateUrl: './toy.component.html',
  styleUrls: ['./toy.component.css'],
})
export class ToyComponent implements OnInit {
  protected toy: Toy
  numberOfItemsInCart: Observable<number>
  hasInStack: Observable<boolean>

  constructor(
    private readonly route: ActivatedRoute,
    private readonly api: PetStoreService,
    public readonly service: OrderService,
    public readonly authService: AuthService,
    private readonly store: Store<AppState>
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params =>
      this.api.getToy(params.get('id')).subscribe(res => {
        this.toy = res
        this.numberOfItemsInCart = this.store.pipe(select(numberOfItems(this.toy.id)))
        this.hasInStack = this.store
          .pipe(select(numberOfItems(this.toy.id)))
          .pipe(map(res => res >= this.toy.quantity))
      })
    )
  }

  removeFromCart() {
    this.store.dispatch(removeFromCart({ id: this.toy.id }))
  }

  addToCart() {
    this.store.dispatch(addToCart({ id: this.toy.id }))
  }
}

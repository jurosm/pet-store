import { Component } from '@angular/core'
import { ActivatedRoute } from '@angular/router'
import { OrderService } from 'src/app/services/order.service'
import { Toy } from 'src/app/models/toy/toy'
import { PetStoreService } from 'src/app/services/pet-store.service'
import { AuthService } from 'src/app/services/auth.service'

@Component({
  selector: 'app-toy',
  templateUrl: './toy.component.html',
  styleUrls: ['./toy.component.css'],
})
export class ToyComponent {
  toy: Toy
  constructor(
    private readonly route: ActivatedRoute,
    private readonly api: PetStoreService,
    public readonly service: OrderService,
    public readonly authService: AuthService
  ) {
    this.route.paramMap.subscribe(params =>
      this.api.getToy(params.get('id')).subscribe(res => {
        this.toy = res
      })
    )
  }

  hasInStack() {
    if (this.toy !== undefined) {
      return this.toy.quantity - this.service.numberOfItems(this.toy.toyId) <= 0
    }
  }
}

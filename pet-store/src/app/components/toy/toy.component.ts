import { Component, OnInit } from '@angular/core';
import { Route } from '@angular/compiler/src/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/services/order.service';
import { Toy } from 'src/app/models/toy/toy';
import { PetStoreService } from 'src/app/services/pet-store.service';

@Component({
  selector: 'app-toy',
  templateUrl: './toy.component.html',
  styleUrls: ['./toy.component.css']
})
export class ToyComponent implements OnInit {

 toy: Toy;
  constructor(private route: ActivatedRoute, private api: PetStoreService, public service: OrderService) {
    this.route.paramMap.subscribe(
      params => this.api.getToy(params.get('id')).subscribe(
        res => {
          this.toy = res;
        }
      )
    );
  }

  ngOnInit() {
  }


}

import { Component, OnInit } from '@angular/core';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { OrderService } from 'src/app/services/order.service';
import { GetToysParams } from 'src/app/models/toy/getToysParams';

@Component({
  selector: 'app-toys',
  templateUrl: './toys.component.html',
  styleUrls: ['./toys.component.css']
})
export class ToysComponent implements OnInit {
  getToyParams: GetToysParams;

  constructor(public api: PetStoreService, public service: OrderService) {
    this.getToyParams = {page: 1, itemsPerPage: 3, matchName: '', order: '', category: ''};
    this.GetToys();
   }

  ngOnInit() {
  }

  GetToys() {
    this.api.getToys(this.getToyParams).subscribe();
  }

}

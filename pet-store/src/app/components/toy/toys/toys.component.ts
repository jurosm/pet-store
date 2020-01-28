import { Component, OnInit } from '@angular/core';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { OrderService } from 'src/app/services/order.service';
import { GetToysParams } from 'src/app/models/toy/getToysParams';
import { ToysResponse } from 'src/app/models/toy/toysResponse';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-toys',
  templateUrl: './toys.component.html',
  styleUrls: ['./toys.component.css']
})
export class ToysComponent implements OnInit {
  searchForm: FormGroup;
  getToyParams: GetToysParams;
  toys: ToysResponse;
  none: string;
  asc: string;
  desc: string;
  all: string;
  default: number;
  numberOfPagesList: number[];
  pageSizeList: number[];

  constructor(public api: PetStoreService, public service: OrderService, public router: Router) {
    this.none = 'none'; this.asc = 'asc'; this.desc = 'desc'; this.all = 'all'; this.default = 1;
    this.pageSizeList = Array(10).fill(0).map((x, i) => i);
    this.searchForm = new FormGroup({
      order: new FormControl(),
      matchName: new FormControl(),
      category: new FormControl(),
      page: new FormControl(),
      pageSize: new FormControl()
    });
    this.toys = new ToysResponse();
    this.getToyParams = {page: 1, pageSize: 6, matchName: '', order: 'none', category: 0};
    this.getToys();
   }

  ngOnInit() {
  }

   getToys() {
    this.api.getToys(this.getToyParams).subscribe(res => {
      this.toys = res;
      this.numberOfPagesList = Array(res.numberOfPages).fill(0).map((x, i) => i);
    }
      );
  }

  paginate(page: number) {
    this.getToyParams.page = page;
    this.getToys();
  }

  filter(){
    this.getToyParams.page = 1;
    this.getToys();
  }

}

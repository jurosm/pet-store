import { Component, OnInit } from '@angular/core'
import { PetStoreService } from 'src/app/services/pet-store.service'
import { OrderService } from 'src/app/services/order.service'
import { GetToysParams } from 'src/app/models/toy/getToysParams'
import { ToysResponse } from 'src/app/models/toy/toysResponse'
import { FormGroup, FormControl } from '@angular/forms'
import { Router } from '@angular/router'
import { AuthService } from 'src/app/services/auth.service'
import { Category } from 'src/app/models/categories/category'

@Component({
  selector: 'app-toys',
  templateUrl: './toys.component.html',
  styleUrls: ['./toys.component.css'],
})
export class ToysComponent implements OnInit {
  searchForm: FormGroup
  getToyParams: GetToysParams
  toys: ToysResponse
  pageSizeList: number[]
  orderIsSuccessful: boolean
  categories: Category[]
  page: number = 1

  constructor(
    public readonly api: PetStoreService,
    public readonly service: OrderService,
    public readonly router: Router,
    public readonly authService: AuthService
  ) {
    this.pageSizeList = Array(9)
      .fill(0)
      .map((_x, i) => i)
    this.pageSizeList = this.pageSizeList.slice(2)

    this.searchForm = new FormGroup({
      order: new FormControl(),
      matchName: new FormControl(),
      category: new FormControl(),
      page: new FormControl(),
      pageSize: new FormControl(),
    })
    this.toys = new ToysResponse()
    this.getToyParams = { offset: 1, limit: 6, matchName: '', order: 0, category: 0 }
  }
  ngOnInit(): void {
    this.getToys()
    this.getCategories()
  }

  getToys() {
    console.log(this.getToyParams.offset)
    if (this.page == 1) {
      this.getToyParams.offset = 1
    } else {
      this.getToyParams.offset = (this.page - 1) * this.getToyParams.limit
    }
    this.api.getToys(this.getToyParams).subscribe(res => {
      this.toys = res
    })
  }

  paginate() {
    this.getToys()
  }

  filter() {
    this.getToyParams.offset = 1
    this.getToys()
  }

  deleteToy(toyId: number) {
    this.api.deleteToy(toyId).subscribe(_res => {
      this.toys.items = this.toys.items.filter(el => el.id !== toyId)
    })
  }

  getCategories() {
    this.api.getCategories().subscribe(res => {
      this.categories = res
    })
  }

  onKeyDown(event) {
    if (event.key === 'enter') {
      this.filter()
    }
  }
}

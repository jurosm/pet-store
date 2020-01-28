import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Toy } from 'src/app/models/toy/toy';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { ToyInfo } from 'src/app/models/toy/toyInfo';
import { Categories } from 'src/app/models/categories/categories';
import { Category } from 'src/app/models/categories/category';

@Component({
  selector: 'app-create-toy',
  templateUrl: './create-toy.component.html',
  styleUrls: ['./create-toy.component.css']
})

export class CreateToyComponent implements OnInit {
all: string;
createForm: FormGroup;
toyInfo: ToyInfo;
categories: Category[];

  constructor(private api: PetStoreService) {
    this.all = 'all';
    this.createForm = new FormGroup({
      name: new FormControl(Validators.required),
      description: new FormControl(Validators.required),
      shortDescription: new FormControl(Validators.required),
      category: new FormControl(Validators.required),
      price: new FormControl(Validators.required),
      quantity: new FormControl(Validators.required)
    });

    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });

    this.toyInfo = new ToyInfo();
  }

  ngOnInit() {
  }

  createToy() {
    this.api.createToy(this.toyInfo).subscribe();
  }

}

import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Toy } from 'src/app/models/toy/toy';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { ToyInfo } from 'src/app/models/toy/toyInfo';
import { Categories } from 'src/app/models/categories/categories';
import { Category } from 'src/app/models/categories/category';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-create-toy',
  templateUrl: './create-toy.component.html',
  styleUrls: ['./create-toy.component.css']
})

export class CreateToyComponent implements OnInit {
all: string;
createForm: FormGroup;
toy: Toy;
category: Category;
categories: Category[];
  isPostEditRoute: boolean;

  constructor(private api: PetStoreService, private route: ActivatedRoute, private router: Router) {
    this.all = 'all';
    this.createForm = new FormGroup({
      name: new FormControl(Validators.required),
      description: new FormControl(Validators.required),
      shortDescription: new FormControl(Validators.required),
      categoryId: new FormControl(Validators.required),
      price: new FormControl(Validators.required),
      quantity: new FormControl(Validators.required)
    });

    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });

    this.toy = new Toy();
  }

  ngOnInit() {
    if (this.router.url.startsWith('/toy/edit')) {
      this.route.paramMap.subscribe(
        params => {this.api.getToy(params.get('id')).subscribe(res => {this.toy = res; this.toy.toyId = +params.get('id'); }); }
      );
    }
  }

  submitToy() {
    if (this.router.url.startsWith('/toy/edit')) {
      this.updateToy();
    } else {
      this.createToy();
    }
  }

  updateToy() {
    this.api.updateToy(this.toy).subscribe();
  }

  createToy() {
    this.api.createToy(this.toy).subscribe();
  }

}

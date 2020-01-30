import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Toy } from 'src/app/models/toy/toy';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { Category } from 'src/app/models/categories/category';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorResponse } from 'src/app/models/error/errorResponse';
import * as Collections from 'typescript-collections';
import { CollectionConverter } from 'src/app/helper/collectionConverter';

@Component({
  selector: 'app-create-toy',
  templateUrl: './create-toy.component.html',
  styleUrls: ['./create-toy.component.css']
})

export class CreateToyComponent implements OnInit {

  createForm: FormGroup;
  toy: Toy;
  category: Category;
  categories: Category[];
  isCreatingSuccessful = false;
  validationServerError: Collections.Dictionary<string, string>;
  serverErrorMessage: string;
  isNotSuccessful = false;

  constructor(private api: PetStoreService, private route: ActivatedRoute, private router: Router) {

    this.createForm = new FormGroup({
      name: new FormControl(Validators.required),
      description: new FormControl(Validators.required),
      shortDescription: new FormControl(Validators.required),
      categoryId: new FormControl(Validators.required),
      price: new FormControl(Validators.required),
      quantity: new FormControl(Validators.required)
    });

    this.toy = new Toy();
    this.validationServerError = new Collections.Dictionary<string, string>();
  }

  ngOnInit() {
    if (this.router.url.startsWith('/toy/edit')) {
      this.route.paramMap.subscribe(
        params => { this.api.getToy(params.get('id')).subscribe(res => { this.toy = res; this.toy.toyId = +params.get('id'); }); }
      );
    }

    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });
  }

  submitToy() {
    if (this.router.url.startsWith('/toy/edit')) {
      this.updateToy();
    } else {
      this.createToy();
    }
  }

  updateToy() {
    this.api.updateToy(this.toy).subscribe(res => {
      this.isCreatingSuccessful = true;
      this.isNotSuccessful = false;
    }, err => {
      this.handleError(err);
    });
  }

  createToy() {
    this.api.createToy(this.toy).subscribe(res => {
      this.isCreatingSuccessful = true;
      this.isNotSuccessful = false;
    }, err => {
      this.handleError(err);
    });
  }

  private handleError(err) {
    if (err instanceof ErrorResponse) {
      this.isNotSuccessful = true;
      this.isCreatingSuccessful = false;
      this.validationServerError = CollectionConverter.errorArrayToDictionary(err.errors);
      this.serverErrorMessage = err.message;
    }
  }

}

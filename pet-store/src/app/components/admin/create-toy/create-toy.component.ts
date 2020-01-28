import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Toy } from 'src/app/models/toy/toy';
import { PetStoreService } from 'src/app/services/pet-store.service';

@Component({
  selector: 'app-create-toy',
  templateUrl: './create-toy.component.html',
  styleUrls: ['./create-toy.component.css']
})
export class CreateToyComponent implements OnInit {
createForm: FormGroup;
toy: Toy;

  constructor(api: PetStoreService) {
    this.createForm = new FormGroup({
      name: new FormControl(Validators.required),
      description: new FormControl(Validators.required),
      shortDescription: new FormControl(Validators.required),
      category: new FormControl(Validators.required),
      price: new FormControl(Validators.required)
    });
    this.toy = new Toy();
  }

  ngOnInit() {
  }

}

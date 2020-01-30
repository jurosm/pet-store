import { Component, OnInit } from '@angular/core';
import { PetStoreService } from 'src/app/services/pet-store.service';
import { Category } from 'src/app/models/categories/category';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoryName } from 'src/app/models/categories/categoryName';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {
 categories: Category[];
 category: Category;
 deleteCategoryForm: FormGroup;
  categoryName: string;
  createCategoryForm: FormGroup;

  constructor(public api: PetStoreService) {
    this.deleteCategoryForm = new FormGroup({
      categoryId: new FormControl()
    });

    this.createCategoryForm = new FormGroup({
      categoryName: new FormControl(Validators.required)
    });

    this.category = new Category();

    this.getCategories();
  }

  ngOnInit() {
  }

  getCategories() {
    this.api.getCategories().subscribe(res => {
      this.categories = res;
    });
  }

  deleteCategory() {
    if (this.category.categoryId !== 0 && this.category.categoryId !== undefined){
      this.api.deleteCategory(this.category.categoryId).subscribe(res => {
        this.getCategories();
      });
    }
  }

  addNewCategory() {
    if(this.categoryName !== undefined) {
    if (this.categoryName.trim() !== '') {
      this.api.createCategory({name: this.categoryName}).subscribe(res => {
        this.getCategories();
      });
    }
  }
}

}

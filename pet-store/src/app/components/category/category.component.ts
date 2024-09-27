import { Component, OnInit } from '@angular/core'
import { PetStoreService } from 'src/app/services/pet-store.service'
import { Category } from 'src/app/models/categories/category'
import { FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css'],
})
export class CategoryComponent implements OnInit {
  categories: Category[]
  deleteCategoryForm: FormGroup
  createCategoryForm: FormGroup

  constructor(private readonly api: PetStoreService) {
    this.deleteCategoryForm = new FormGroup({
      categoryId: new FormControl(Validators.required),
    })

    this.createCategoryForm = new FormGroup({
      categoryName: new FormControl(),
    })
  }
  ngOnInit(): void {
    this.getCategories()
  }

  getCategories() {
    this.api.getCategories().subscribe(res => {
      this.categories = res
    })
  }

  deleteCategory() {
    const deleteCategoryId = this.deleteCategoryForm.value.categoryId

    if (deleteCategoryId !== 0 && deleteCategoryId !== undefined) {
      this.api.deleteCategory(deleteCategoryId).subscribe(_res => {
        this.getCategories()
      })
    }
  }

  addNewCategory() {
    const categoryName = this.createCategoryForm.value.categoryName
    if (categoryName.trim() !== '') {
      this.api.createCategory({ name: categoryName }).subscribe(_res => {
        this.getCategories()
      })
    }
  }
}

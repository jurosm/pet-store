import { Injectable } from '@angular/core'
import { HttpClient, HttpParams } from '@angular/common/http'
import { Order } from '../models/order/order'
import { Observable } from 'rxjs'
import { catchError } from 'rxjs/operators'
import { LoginModel } from '../models/auth/loginModel'
import { AuthResult } from '../models/auth/authResult'
import { ErrorService } from './errorService'
import { GetToysParams } from '../models/toy/getToysParams'
import { environment } from '../../../src/environments/environment'
import { ToysResponse } from '../models/toy/toysResponse'
import { Toy } from '../models/toy/toy'
import { Category } from '../models/categories/category'
import { OrderListItem } from '../models/order/orderListItem'
import { CommentsUnit } from '../models/toy/comments/commentsUnit'
import { CommentData } from '../models/toy/comments/commentData'
import { CategoryName } from '../models/categories/categoryName'

@Injectable({
  providedIn: 'root',
})
export class PetStoreService {
  private readonly url: string
  constructor(
    private readonly httpClient: HttpClient,
    private readonly errorService: ErrorService
  ) {
    this.url = environment.apiUrl
  }

  create(order: Order) {
    return this.httpClient.post(`${this.url}/order`, order)
  }

  login(model: LoginModel): Observable<AuthResult> {
    return this.httpClient
      .post<AuthResult>(`${this.url}/auth/login`, model)
      .pipe(catchError(this.errorService.handlerError))
  }

  getToys(getToysParams: GetToysParams): Observable<ToysResponse> {
    let params = new HttpParams()
      .set('page', getToysParams.page.toString())
      .set('pageSize', getToysParams.pageSize.toString())

    if (getToysParams.order !== 0) {
      params = params.set('order', getToysParams.order.toString())
    }
    if (getToysParams.category !== 0) {
      params = params.set('categoryId', getToysParams.category.toString())
    }
    if (getToysParams.matchName.trim().length > 0) {
      params = params.set('match', getToysParams.matchName)
    }

    return this.httpClient.get<ToysResponse>(`${this.url}/toy`, {
      params,
    })
  }

  getToy(id: string): Observable<Toy> {
    return this.httpClient.get<Toy>(`${this.url}/toy/${id}`)
  }

  createToy(toy: Toy) {
    return this.httpClient
      .post<Toy>(`${this.url}/toy`, toy)
      .pipe(catchError(this.errorService.handlerError))
  }

  getCategories() {
    return this.httpClient.get<Category[]>(`${this.url}/category`)
  }

  deleteToy(toyId: number) {
    return this.httpClient.delete(`${this.url}/toy/${toyId}`)
  }

  updateToy(toy: Toy) {
    return this.httpClient
      .put<Toy>(`${this.url}/toy/${toy.toyId}`, toy)
      .pipe(catchError(this.errorService.handlerError))
  }

  getOrders() {
    return this.httpClient.get<OrderListItem[]>(`${this.url}/order`)
  }

  createComment(comment: CommentData) {
    return this.httpClient.post<Comment>(`${this.url}/comment`, comment)
  }

  getComments(toyId: number) {
    return this.httpClient.get<CommentsUnit[]>(`${this.url}/comment/${toyId}`)
  }

  deleteCategory(id: number) {
    return this.httpClient.delete(`${this.url}/category/${id}`)
  }

  createCategory(name: CategoryName) {
    return this.httpClient.post<string>(`${this.url}/category`, name)
  }
}

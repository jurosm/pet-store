import { Injectable } from '@angular/core';
import { PetStoreService } from './pet-store.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper: JwtHelperService, private service: PetStoreService) { }

  isAuthenticated() {
    let token = localStorage.getItem('jwtToken');
    if (token !== 'undefined' && token != null) {
      return !this.jwtHelper.isTokenExpired(token);
    } else {
      return false;
    }
  }

  getJwtToken() {
    return localStorage.getItem('jwtToken');
  }


  logout() {
    localStorage.removeItem('jwtToken');
  }

}

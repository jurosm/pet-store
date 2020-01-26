import { HttpRequest, HttpHeaders, HttpInterceptor, HttpEvent, HttpHandler } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()

export class TokenInterceptor implements HttpInterceptor {

  constructor(public authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()) {
      return next.handle(this.addToken(req));
    } else {
      return next.handle(req);
    }
  }

  private addToken(req: HttpRequest<any>): HttpRequest<any> {
    const headers = new HttpHeaders({
      'Authorization' : 'Bearer ' + this.authService.getJwtToken()
    });
    return req.clone({
      headers
    });
  }
}

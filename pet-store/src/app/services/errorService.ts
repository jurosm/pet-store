import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ErrorResponse } from '../models/error/errorResponse';
import { throwError } from 'rxjs';

@Injectable({
    providedIn: 'root'
})

export class ErrorService {
    constructor(private router: Router) { }

    handlerError = (error) => {
        if (error instanceof HttpErrorResponse && error.status < 500) {
            const err = new ErrorResponse();
            if (error.error.errors !== undefined) {
                err.errors = error.error.errors;
            }
            err.message = error.error.message; const a = 3;
            return throwError(err);
        } else if (error instanceof HttpErrorResponse) {
            this.router.navigate(['/error']);
        } else { return throwError(error); }
    }
}

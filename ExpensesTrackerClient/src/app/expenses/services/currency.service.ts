import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { ICurrency } from '../models/currency';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private readonly baseApiUrl: string = 'https://localhost:44330/api/';
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) { }

  getCurrencies(): Observable<ICurrency[]> {
    return this.http.get<ICurrency[]>(this.baseApiUrl + 'Currency')
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  private handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }

    console.error(errorMessage);
    
    return throwError(errorMessage);
  }
}
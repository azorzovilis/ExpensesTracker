import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, retry } from 'rxjs/operators';

import { IExpense } from '../models/expense';
import { ExpenseType } from '../models/expenseType';
import { ICurrency } from '../models/currency';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private readonly expensesApiUrl: string = 'https://localhost:44330/api/';
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) { }

  getExpenses(): Observable<IExpense[]> {
    return this.http.get<IExpense[]>(this.expensesApiUrl + 'expenses/')
      .pipe(
        retry(1),
        tap(data => console.log('All: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getExpense(id: number): Observable<IExpense> {
    return this.http.get<IExpense>(this.expensesApiUrl + 'expenses/' + id)
      .pipe(
        retry(1),
        tap(data => console.log('All: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getCurrencies(): Observable<ICurrency[]> {
    return this.http.get<ICurrency[]>(this.expensesApiUrl + 'Currency')
      .pipe(
        tap(data => console.log('All: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  getExpenseTypes(): Observable<ExpenseType[]> {
    return this.http.get<ExpenseType[]>(this.expensesApiUrl + 'expenses/types')
      .pipe(
        tap(data => console.log('All: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  createExpense(expense): Observable<IExpense> {
    return this.http.post<IExpense>(this.expensesApiUrl + 'expenses/', JSON.stringify(expense), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  updateExpense(expenseId: number, expense): Observable<IExpense> {
    return this.http.put<IExpense>(this.expensesApiUrl + 'expenses/' + expenseId, JSON.stringify(expense), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  deleteExpense(expenseId: number): Observable<IExpense> {
    return this.http.delete<IExpense>(this.expensesApiUrl + 'expenses/' + expenseId)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  private handleError(error) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${error.status}, error message is: ${error.message}`;
    }

    console.error(errorMessage);
    
    return throwError(errorMessage);
  }
}
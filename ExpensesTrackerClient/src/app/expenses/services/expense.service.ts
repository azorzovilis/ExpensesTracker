import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map, retry } from 'rxjs/operators';

import { IExpense } from '../models/expense';
import { ExpenseType } from '../models/expenseType';

@Injectable({
  providedIn: 'root'
})
export class ExpenseService {
  private readonly baseApiUrl: string = 'https://localhost:44330/api/';
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private http: HttpClient) { }

  getExpenses(): Observable<IExpense[]> {
    return this.http.get<IExpense[]>(this.baseApiUrl + 'expenses/')
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  getExpense(id: number): Observable<IExpense> {
    return this.http.get<IExpense>(this.baseApiUrl + 'expenses/' + id)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  getExpenseTypes(): Observable<ExpenseType[]> {
    return this.http.get<ExpenseType[]>(this.baseApiUrl + 'expenses/types')
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  createExpense(expense): Observable<IExpense> {
    return this.http.post<IExpense>(this.baseApiUrl + 'expenses/', JSON.stringify(expense), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  updateExpense(expenseId: number, expense): Observable<IExpense> {
    return this.http.put<IExpense>(this.baseApiUrl + 'expenses/' + expenseId, JSON.stringify(expense), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  deleteExpense(expenseId: number): Observable<IExpense> {
    return this.http.delete<IExpense>(this.baseApiUrl + 'expenses/' + expenseId)
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
import { Component, OnInit } from '@angular/core';

import { IExpense } from '../models/expense';
import { ExpenseService } from '../services/expense.service';

@Component({
  selector: 'expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.less']
})

export class ExpenseListComponent implements OnInit {
  editField: string;
  pageTitle = 'Expenses';
  errorMessage = '';

  _listFilter = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredExpenses = this.listFilter ? this.performFilter(this.listFilter) : this.expenses;
  }

  filteredExpenses: IExpense[] = [];
  expenses: IExpense[] = [];

  constructor(private expenseService: ExpenseService) {

  }

  loadExpenses() {
    this.expenseService.getExpenses().subscribe({
      next: expenses => {
        this.expenses = expenses;
        this.filteredExpenses = this.expenses;
      },
      error: err => this.errorMessage = err
    });
  }

  performFilter(filterBy: string): IExpense[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.expenses.filter((expense: IExpense) =>
      expense.recipient.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  delete(expenseId: number) {
    const ans = confirm('Do you want to delete expense with Id: ' + expenseId);
    if (ans) {
      this.expenseService.deleteExpense(expenseId).subscribe((data) => {
        this.loadExpenses();
      });;
    }
  }

  ngOnInit(): void {
    this.loadExpenses();
  }
}
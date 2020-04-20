import { Component, OnInit } from '@angular/core';

import { IExpense } from '../../models/expense';
import { ExpenseService } from '../expense.service';

@Component({
  selector: 'expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.less']
})

export class ExpenseListComponent implements OnInit {
  editField: string;
  pageTitle = 'Expenses';
  errorMessage = '';

  expenses: IExpense[];

  constructor(private expenseService: ExpenseService) {

  }

  loadExpenses() {
    this.expenseService.getExpenses().subscribe({
      next: expenses => {
        this.expenses = expenses;
      },
      error: err => this.errorMessage = err
    });
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
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { IExpense } from '../models/expense';
import { ExpenseService } from '../services/expense.service';

@Component({
  templateUrl: './expense-detail.component.html',
  styleUrls: ['./expense-detail.component.less']
})
export class ExpenseDetailComponent implements OnInit {
  pageTitle = 'Expense Detail';
  errorMessage = '';
  expense: IExpense | undefined;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private expenseService: ExpenseService) {
  }

  ngOnInit() {
    const param = this.route.snapshot.paramMap.get('id');
    if (param) {
      const id = +param;
      this.getExpense(id);
    }
  }

  getExpense(id: number) {
    this.expenseService.getExpense(id).subscribe({
      next: expense => this.expense = expense,
      error: err => this.errorMessage = err
    });
  }

  onBack(): void {
    this.router.navigate(['/']);
  }
}

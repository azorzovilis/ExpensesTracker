import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ExpenseService } from '../services/expense.service';
import { CurrencyService } from '../services/currency.service';
import { IExpense } from '../models/expense';
import { ICurrency } from '../models/currency';
import { ExpenseType } from '../models/expenseType';

@Component({
  selector: 'app-expense-add-edit',
  templateUrl: './expense-add-edit.component.html',
  styleUrls: ['./expense-add-edit.component.less']
})
export class ExpenseAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formRecipientName: string;
  formTransactionDate: string;
  formAmount: string;
  formCurrency: string;
  formExpenseType: string;
  expenseId: number;
  errorMessage: any;
  existingExpense: IExpense;
  currencies: ICurrency[];
  expenseTypes: ExpenseType[];

  constructor(private expenseService: ExpenseService,
    private currencyService: CurrencyService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formRecipientName = 'recipient';
    this.formTransactionDate = 'transactionDate';
    this.formAmount = 'amount';
    this.formCurrency = 'currency';
    this.formExpenseType = 'expenseType';

    if (this.avRoute.snapshot.params[idParam]) {
      this.expenseId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        expenseId: 0,
        transactionDate: [new Date(), [Validators.required]],
        recipient: ['', [Validators.required]],
        amount: ['', [Validators.required]],
        currency: ['', [Validators.required]],
        expenseType: ['', [Validators.required]]
      }
    )
  }

  ngOnInit() {
    this.currencyService.getCurrencies().subscribe(
      (response: ICurrency[]) => this.currencies = response
    );

    this.expenseService.getExpenseTypes().subscribe(
      (response: ExpenseType[]) => this.expenseTypes = response
    );

    if (this.expenseId > 0) {
      this.actionType = 'Edit';
      this.expenseService.getExpense(this.expenseId)
        .subscribe((response: IExpense) => {
          this.existingExpense = response,
            this.form.controls[this.formAmount].setValue(response.amount),
            this.form.controls[this.formRecipientName].setValue(response.recipient),
            this.form.controls[this.formCurrency].setValue(response.currency),
            this.form.controls[this.formExpenseType].setValue(response.expenseType.expenseTypeId),
            this.form.controls[this.formTransactionDate].setValue(this.transformDate(response.transactionDate))
        }, error => console.error(error));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let expense: IExpense = {
        transactionDate: this.form.get(this.formTransactionDate).value,
        recipient: this.form.get(this.formRecipientName).value,
        amount: +this.form.get(this.formAmount).value,
        currency: this.form.get(this.formCurrency).value,
        expenseTypeId: +this.form.get(this.formExpenseType).value
      };

      this.expenseService.createExpense(expense)
        .subscribe((data) => {
          this.router.navigate(['/expenses', data.expenseId]);
        });
    }

    if (this.actionType === 'Edit') {
      let expense: IExpense = {
        expenseId: this.existingExpense.expenseId,
        transactionDate: this.form.get(this.formTransactionDate).value,
        recipient: this.form.get(this.formRecipientName).value,
        amount: +this.form.get(this.formAmount).value,
        currency: this.form.get(this.formCurrency).value,
        expenseTypeId: +this.form.get(this.formExpenseType).value
      };
      this.expenseService.updateExpense(expense.expenseId, expense)
        .subscribe(() => {
          this.router.navigate(['/']);
        });
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }

  //TODO: remove this hack and use bootstrap datepicker 
  transformDate(aDate: string | number | Date) : string{
    var date = new Date(aDate);
    return new Date(date.getTime() - (date.getTimezoneOffset() * 60000 ))
            .toISOString()
            .split("T")[0];
  }

  get recipient() { return this.form.get(this.formRecipientName); }
  get transactionDate() { return this.form.get(this.formTransactionDate); }
  get amount() { return this.form.get(this.formAmount); }
  get currency() { return this.form.get(this.formCurrency); }
  get expenseType() { return this.form.get(this.formExpenseType); }
}
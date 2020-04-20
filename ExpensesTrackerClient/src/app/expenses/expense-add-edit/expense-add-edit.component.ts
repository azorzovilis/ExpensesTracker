import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { ExpenseService } from '../expense.service';
import { IExpense } from '../../models/expense';
import { ICurrency } from '../../models/currency';
import { ExpenseType } from '../../models/expenseType';

@Component({
  selector: 'app-expense-add-edit',
  templateUrl: './expense-add-edit.component.html',
  styleUrls: ['./expense-add-edit.component.less']
})
export class ExpenseAddEditComponent implements OnInit {
  form: FormGroup;
  actionType: string;
  formRecipientName: string;
  formAmount: string;
  formCurrency: string;
  formExpenseType: string;
  expenseId: number;
  errorMessage: any;
  existingExpense: IExpense;
  currencies: ICurrency[];
  expenseTypes: ExpenseType[];

  constructor(private expenseService: ExpenseService,
    private formBuilder: FormBuilder,
    private avRoute: ActivatedRoute,
    private router: Router) {
    const idParam = 'id';
    this.actionType = 'Add';
    this.formRecipientName = 'recipient';
    this.formAmount = 'amount';
    this.formCurrency = 'currency';
    this.formExpenseType = 'expenseType';

    if (this.avRoute.snapshot.params[idParam]) {
      this.expenseId = this.avRoute.snapshot.params[idParam];
    }

    this.form = this.formBuilder.group(
      {
        expenseId: 0,
        recipient: ['', [Validators.required]],
        amount: ['', [Validators.required]],
        currency: ['', [Validators.required]],
        expenseType: ['', [Validators.required]]
      }
    )
  }

  ngOnInit() {

    this.expenseService.getCurrencies().subscribe(
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
            this.form.controls[this.formCurrency].setValue(response.currency);
            this.form.controls[this.formExpenseType].setValue(response.expenseType.expenseTypeId)
        }, error => console.error(error));
    }
  }

  save() {
    if (!this.form.valid) {
      return;
    }

    if (this.actionType === 'Add') {
      let expense: IExpense = {
        transactionDate: new Date(),  //todo add date picker
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
        //transactionDate: this.form.get(this.formTransactionDate).value,
        transactionDate: new Date(), //todo Remove
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

  get recipient() { return this.form.get(this.formRecipientName); }
  get amount() { return this.form.get(this.formAmount); }
  get currency() { return this.form.get(this.formCurrency); }
  get expenseType() { return this.form.get(this.formExpenseType); }

}
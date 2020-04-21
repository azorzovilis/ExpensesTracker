import { ExpenseType } from "./expenseType";

export interface IExpense {
  expenseId?: number;
  transactionDate: Date;
  amount: number;
  recipient: string;
  currency: string;
  expenseTypeId: number;
  expenseType?: ExpenseType;
}
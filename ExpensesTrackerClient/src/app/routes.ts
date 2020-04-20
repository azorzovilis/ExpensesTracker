import { Routes } from '@angular/router';
import { ExpenseDetailComponent } from './expenses/expense-detail/expense-detail.component';
import { ExpenseListComponent } from './expenses/expense-list/expense-list.component';
import { ExpenseAddEditComponent } from './expenses/expense-add-edit/expense-add-edit.component';

export const appRoutes: Routes = [
    { path: '', component: ExpenseListComponent, pathMatch: 'full' },
    { path: 'expenses/:id', component: ExpenseDetailComponent },
    { path: 'add', component: ExpenseAddEditComponent },
    { path: 'expense/edit/:id', component: ExpenseAddEditComponent }
]
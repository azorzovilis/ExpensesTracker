import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';

import { ExpenseDetailComponent } from './expenses/expense-detail/expense-detail.component';
import { ExpenseListComponent } from './expenses/expense-list/expense-list.component';
import { ExpenseAddEditComponent } from './expenses/expense-add-edit/expense-add-edit.component';
import { appRoutes } from './routes';

@NgModule({
  declarations: [
    AppComponent,
    ExpenseListComponent,
    ExpenseDetailComponent,
    ExpenseAddEditComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule.forRoot(appRoutes, {
      enableTracing: false // for Debugging of the Routes
      }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

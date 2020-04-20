import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseAddEditComponent } from './expense-add-edit.component';

describe('ExpenseAddEditComponent', () => {
  let component: ExpenseAddEditComponent;
  let fixture: ComponentFixture<ExpenseAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExpenseAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
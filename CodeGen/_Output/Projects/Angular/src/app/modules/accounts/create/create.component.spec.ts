import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountsCreateComponent } from './create.component';

describe('AccountsCreateComponent', () => {
  let component: AccountsCreateComponent;
  let fixture: ComponentFixture<AccountsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccountsCreateComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

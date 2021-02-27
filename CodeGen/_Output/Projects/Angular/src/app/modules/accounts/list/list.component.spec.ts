import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModulesAccountsListComponent } from './list.component';

describe('ModulesAccountsListComponent', () => {
  let component: ModulesAccountsListComponent;
  let fixture: ComponentFixture<ModulesAccountsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModulesAccountsListComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModulesAccountsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

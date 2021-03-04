import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModulesContactsListComponent } from './list.component';

describe('ModulesContactsListComponent', () => {
  let component: ModulesContactsListComponent;
  let fixture: ComponentFixture<ModulesContactsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ModulesContactsListComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ModulesContactsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

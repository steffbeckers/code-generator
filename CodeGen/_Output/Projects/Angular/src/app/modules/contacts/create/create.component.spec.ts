import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactsCreateComponent } from './create.component';

describe('ContactsCreateComponent', () => {
  let component: ContactsCreateComponent;
  let fixture: ComponentFixture<ContactsCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContactsCreateComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactsCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

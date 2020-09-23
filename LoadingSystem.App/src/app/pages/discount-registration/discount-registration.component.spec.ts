import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DiscountRegistrationComponent } from './discount-registration.component';

describe('DiscountRegistrationComponent', () => {
  let component: DiscountRegistrationComponent;
  let fixture: ComponentFixture<DiscountRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DiscountRegistrationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DiscountRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

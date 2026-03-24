import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarCatalog } from './car-catalog';

describe('CarCatalog', () => {
  let component: CarCatalog;
  let fixture: ComponentFixture<CarCatalog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarCatalog],
    }).compileComponents();

    fixture = TestBed.createComponent(CarCatalog);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBuisnessComponent } from './add-buisness.component';

describe('AddBuisnessComponent', () => {
  let component: AddBuisnessComponent;
  let fixture: ComponentFixture<AddBuisnessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBuisnessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBuisnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

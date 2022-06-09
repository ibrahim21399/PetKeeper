import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBuisnessComponent } from './edit-buisness.component';

describe('EditBuisnessComponent', () => {
  let component: EditBuisnessComponent;
  let fixture: ComponentFixture<EditBuisnessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditBuisnessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditBuisnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

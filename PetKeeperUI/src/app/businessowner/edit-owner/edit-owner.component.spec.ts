import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOwnerComponent } from './edit-owner.component';

describe('EditOwnerComponent', () => {
  let component: EditOwnerComponent;
  let fixture: ComponentFixture<EditOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditOwnerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

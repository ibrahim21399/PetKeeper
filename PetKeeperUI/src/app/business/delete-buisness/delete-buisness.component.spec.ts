import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteBuisnessComponent } from './delete-buisness.component';

describe('DeleteBuisnessComponent', () => {
  let component: DeleteBuisnessComponent;
  let fixture: ComponentFixture<DeleteBuisnessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteBuisnessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteBuisnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

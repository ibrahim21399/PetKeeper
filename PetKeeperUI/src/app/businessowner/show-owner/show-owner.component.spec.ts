import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowOwnerComponent } from './show-owner.component';

describe('ShowOwnerComponent', () => {
  let component: ShowOwnerComponent;
  let fixture: ComponentFixture<ShowOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowOwnerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

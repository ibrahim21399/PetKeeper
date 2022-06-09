import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowBuisnessComponent } from './show-buisness.component';

describe('ShowBuisnessComponent', () => {
  let component: ShowBuisnessComponent;
  let fixture: ComponentFixture<ShowBuisnessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowBuisnessComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowBuisnessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAppointmentsComponent } from './show-appointments.component';

describe('ShowAppointmentsComponent', () => {
  let component: ShowAppointmentsComponent;
  let fixture: ComponentFixture<ShowAppointmentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAppointmentsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowAppointmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

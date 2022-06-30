import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ScheduleClientComponent } from './schedule-client.component';

describe('ScheduleClientComponent', () => {
  let component: ScheduleClientComponent;
  let fixture: ComponentFixture<ScheduleClientComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ScheduleClientComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ScheduleClientComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

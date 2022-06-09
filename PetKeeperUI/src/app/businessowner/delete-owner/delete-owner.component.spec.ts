import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteOwnerComponent } from './delete-owner.component';

describe('DeleteOwnerComponent', () => {
  let component: DeleteOwnerComponent;
  let fixture: ComponentFixture<DeleteOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteOwnerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SensorRegistrationComponent } from './sensor-registration.component';

describe('SensorRegistrationComponent', () => {
  let component: SensorRegistrationComponent;
  let fixture: ComponentFixture<SensorRegistrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SensorRegistrationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SensorRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

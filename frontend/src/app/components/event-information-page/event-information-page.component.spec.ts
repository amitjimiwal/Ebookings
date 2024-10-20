import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventInformationPageComponent } from './event-information-page.component';

describe('EventInformationPageComponent', () => {
  let component: EventInformationPageComponent;
  let fixture: ComponentFixture<EventInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventInformationPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EventInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

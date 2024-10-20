import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketBoxComponent } from './ticket-box.component';

describe('TicketBoxComponent', () => {
  let component: TicketBoxComponent;
  let fixture: ComponentFixture<TicketBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketBoxComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TicketBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

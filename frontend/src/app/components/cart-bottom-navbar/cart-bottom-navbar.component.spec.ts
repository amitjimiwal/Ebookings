import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CartBottomNavbarComponent } from './cart-bottom-navbar.component';

describe('CartBottomNavbarComponent', () => {
  let component: CartBottomNavbarComponent;
  let fixture: ComponentFixture<CartBottomNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CartBottomNavbarComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CartBottomNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

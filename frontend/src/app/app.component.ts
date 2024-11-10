import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { EventsListComponent } from './components/events-page/events-list.component';
import { EventBookingComponent } from './components/event-booking/event-booking.component';
import { SuccessMessageComponent } from './components/booking-success/success-message.component';
import { EventDescriptionComponent } from './components/event-description-box/event-description.component';
import { EventComponent } from './components/event-card/event.component';
import { AuthService } from './services/auth/auth-service.service';
import { LoginComponent } from './components/auth/login/login.component';
import { EventInformationPageComponent } from './components/event-information-page/event-information-page.component';
import { TicketBoxComponent } from './components/ticket-box/ticket-box.component';
import { faCartShopping } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CartService } from './services/cart/cart.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EventsListComponent, EventBookingComponent, SuccessMessageComponent, RouterModule, EventDescriptionComponent, EventComponent, LoginComponent, EventDescriptionComponent, EventInformationPageComponent, TicketBoxComponent, FontAwesomeModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  authService = inject(AuthService);
  constructor(private router: Router, private cartService: CartService) {
  }
  ngOnInit(): void {
    this.cartService.cart$.subscribe((cart) => {
      this.cartItems = cart?.totalTickets || 0;
    });
  }
  cartItems = 0;
  cartIcon = faCartShopping;
  title = 'EBookings';
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

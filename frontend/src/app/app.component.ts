import { Component, inject } from '@angular/core';
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

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EventsListComponent, EventBookingComponent, SuccessMessageComponent, RouterModule, EventDescriptionComponent, EventComponent, LoginComponent, EventDescriptionComponent, EventInformationPageComponent, TicketBoxComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  authService = inject(AuthService);
  constructor(private router: Router) {
  }
  title = 'EBookings';
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}

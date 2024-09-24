import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { EventsListComponent } from './components/events-list/events-list.component';
import { EventBookingComponent } from './components/event-booking/event-booking.component';
import { SuccessMessageComponent } from './components/booking-success/success-message.component';
import { EventDescriptionComponent } from './components/event-description/event-description.component';
import { EventComponent } from './components/event-card/event.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, EventsListComponent, EventBookingComponent, SuccessMessageComponent, RouterModule, EventDescriptionComponent, EventComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'EBookings';
}

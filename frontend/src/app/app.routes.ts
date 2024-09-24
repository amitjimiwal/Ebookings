import { Routes } from '@angular/router';
import { EventsListComponent } from './components/events-list/events-list.component';
import { EventBookingComponent } from './components/event-booking/event-booking.component';
import { SuccessMessageComponent } from './components/booking-success/success-message.component';

export const routes: Routes = [
     {
          path: '',
          redirectTo: 'events',
          pathMatch: 'full'
     }, {
          path: 'events',
          component: EventsListComponent
     }, {
          path: 'booking/:id',
          component: EventBookingComponent
     }, {
          path: 'success/:bookingId',
          component: SuccessMessageComponent
     }
];

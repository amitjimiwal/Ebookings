import { Routes } from '@angular/router';
import { EventsListComponent } from './components/events-list/events-list.component';
import { EventBookingComponent } from './components/event-booking/event-booking.component';
import { SuccessMessageComponent } from './components/booking-success/success-message.component';
import { LoginComponent } from './components/auth/login/login.component';
import { RegisterComponent } from './components/auth/register/register.component';
import { AuthGuard } from './guards/auth.guard';
import { ProfileComponent } from './components/profile/profile.component';

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
          component: EventBookingComponent,
          canActivate: [AuthGuard]
     }, {
          path: 'success/:bookingId',
          component: SuccessMessageComponent,
          canActivate: [AuthGuard]
     }, {
          path: 'login',
          component: LoginComponent
     }, {
          path: 'signup',
          component: RegisterComponent,
     }, {
          path: 'profile',
          component: ProfileComponent,
          canActivate: [AuthGuard]
     }
];

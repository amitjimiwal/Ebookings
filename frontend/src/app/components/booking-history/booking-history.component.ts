import { Component, OnInit } from '@angular/core';
import { EventBooking } from '../../models/interface/booking';
import { BookingService } from '../../services/booking/booking.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-booking-history',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './booking-history.component.html',
  styleUrl: './booking-history.component.css'
})
export class BookingHistoryComponent implements OnInit {
  bookingHistory: EventBooking[] = [];
  constructor(private bookingService: BookingService) { }
  ngOnInit(): void {
    this.bookingService.getUserBookings().subscribe((bookings) => {
      this.bookingHistory = bookings;
    }, (error) => {
      console.error('Error occurred while fetching booking history', error);
    });
  }
}
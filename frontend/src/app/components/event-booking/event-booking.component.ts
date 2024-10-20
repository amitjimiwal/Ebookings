import { Component, inject, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule, RouterState } from '@angular/router';
import { EventService } from '../../services/event/event.service';
import { CommonModule } from '@angular/common';
import { EventDescriptionComponent } from '../event-description/event-description.component';
import { BookingService } from '../../services/booking/booking.service';

@Component({
  selector: 'app-event-booking',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, EventDescriptionComponent],
  templateUrl: './event-booking.component.html',
  styleUrl: './event-booking.component.css'
})
export class EventBookingComponent implements OnInit {
  event: EventData | undefined = undefined;
  bookingForm: FormGroup;
  eventService = inject(EventService);
  totalPrice: number = 0;
  bookingService = inject(BookingService);


  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder, private router: Router) {
    this.bookingForm = formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', Validators.required],
      noOfTickets: [1, [Validators.required, Validators.min(1)]]
    });

    // subscribe event to ticket changes
    this.bookingForm.get('noOfTickets')?.valueChanges.subscribe(() => {
      this.calculateTotalAmount();
    });
  }
  ngOnInit(): void {
    const eventId = this.route.snapshot.params['id'];
    // Fetch the corresponding event data
    this.eventService.getEventById(eventId).subscribe(event => {
      if (event) this.event = event;
      // this.totalPrice = this.event?.ticketPrice || 0;
    }, (error) => {
      console.error(`Error occurred while getting the data of event with id ${eventId}`, error);
    }, () => {
      console.log(`Event with id ${eventId} data fetched successfully`);
    });
  }
  // Function to calculate/update the total amount
  calculateTotalAmount(): void {
    if (this.event) {
      let tickets = this.bookingForm.get('noOfTickets')?.value || 1;
      // this.totalPrice = tickets * this.event.ticketPrice;
    }
    console.log('Total amount updated to:', this.totalPrice);
  }

  onSubmit(): void {
    if (this.event && this.bookingForm.valid) {
      const payload = { ...this.bookingForm.value, eventId: this.event.id, totalPrice: this.totalPrice };
      this.bookingService.postBooking(payload).subscribe({
        next: (response) => {
          if (response) {
            console.log('Form submitted successfully');
            console.log(response);
            this.router.navigate([`/success/${response.id}`]);
          } else {
            console.error('Form submission failed');
            console.log(response.message);
            alert(response.error);
          }
        },
        error: (error) => {
          // Handle error
          console.error('Error submitting form', error);
          alert(error.error);
        }
      });
    } else {
      console.error('Booking form is invalid');
    }
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, RouterModule, RouterState } from '@angular/router';
import { EventService } from '../../services/event.service';
import { CommonModule } from '@angular/common';
import { EventDescriptionComponent } from '../event-description/event-description.component';

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
  totalAmount: number = 0;
  constructor(private route: ActivatedRoute, private formBuilder: FormBuilder) {
    this.bookingForm = formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      tickets: [1, [Validators.required, Validators.min(1)]]
    });

    // subscribe event to ticket changes
    this.bookingForm.get('tickets')?.valueChanges.subscribe(() => {
      this.calculateTotalAmount();
    });
  }
  ngOnInit(): void {
    const eventId = this.route.snapshot.params['id'];

    // Fetch the corresponding event data
    this.eventService.getEventById(eventId).subscribe(event => {
      if (event) this.event = event;
      this.totalAmount = this.event?.ticketPrice || 0;
    }, (error) => {
      console.error(`Error occurred while getting the data of event with id ${eventId}`, error);
    }, () => {
      console.log(`Event with id ${eventId} data fetched successfully`);
    });
  }

  // Function to calculate/update the total amount
  calculateTotalAmount(): void {
    if (this.event) {
      let tickets = this.bookingForm.get('tickets')?.value || 1;
      this.totalAmount = tickets * this.event.ticketPrice;
    }
    console.log('Total amount updated to:', this.totalAmount);
  }

  onSubmit(): void {
    if (this.event && this.bookingForm.valid) {
      console.log('Booking form submitted with data:', this.bookingForm.value);
    } else {
      console.error('Booking form is invalid');
    }
  }
}

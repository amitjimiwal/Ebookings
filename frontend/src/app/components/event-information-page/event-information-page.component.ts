import { Component, inject, Input, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { EventDescriptionComponent } from '../event-description-box/event-description.component';
import { TicketBoxComponent } from '../ticket-box/ticket-box.component';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from '../../services/event/event.service';

@Component({
  selector: 'app-event-information-page',
  standalone: true,
  imports: [EventDescriptionComponent, TicketBoxComponent],
  templateUrl: './event-information-page.component.html',
  styleUrl: './event-information-page.component.css'
})
export class EventInformationPageComponent implements OnInit {
  eventData: EventData | undefined = undefined;
  @Input() event: EventData | undefined = undefined;
  eventService = inject(EventService);
  constructor(private route: ActivatedRoute, private router: Router) {
  }
  ngOnInit(): void {
    const eventId = this.route.snapshot.params['id'];
    // Fetch the corresponding event data
    this.eventService.getEventById(eventId).subscribe(event => {
      if (event) this.eventData = event;
      // this.totalPrice = this.event?.ticketPrice || 0;
    }, (error) => {
      console.error(`Error occurred while getting the data of event with id ${eventId}`, error);
    }, () => {
      console.log(`Event with id ${eventId} data fetched successfully`);
    });
  }
}

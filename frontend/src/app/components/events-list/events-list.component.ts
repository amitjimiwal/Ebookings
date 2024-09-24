import { Component, inject, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { EventService } from '../../services/event/event.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCalendarAlt, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { EventComponent } from '../event-card/event.component';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [CommonModule, RouterModule, EventComponent],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit {
  venueIcon = faLocationDot;
  calenderIcon = faCalendarAlt;
  public events: EventData[] = [];
  private eventService = inject(EventService);
  ngOnInit(): void {
    this.eventService.getEvents().subscribe(events => this.events = events, (error) => {
      console.error("Error occurred while getting the data of events", error);
    }, () => {
      console.log("Events data fetched successfully");
    });
  }
}

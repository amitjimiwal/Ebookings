import { Component, Input } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { faCalendarAlt, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'event-description',
  standalone: true,
  imports: [FontAwesomeModule],
  templateUrl: './event-description.component.html',
  styleUrl: './event-description.component.css'
})
export class EventDescriptionComponent {
  @Input() event?: EventData;
  venueIcon = faLocationDot
  calenderIcon = faCalendarAlt
}

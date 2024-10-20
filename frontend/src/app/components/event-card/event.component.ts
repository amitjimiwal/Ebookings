import { Component, Input, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EventData } from '../../models/interface/event';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCalendarAlt, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { CarouselComponent } from '../carousel/carousel.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'event-card',
  standalone: true,
  imports: [RouterModule, FontAwesomeModule, CarouselComponent, CommonModule],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent {
  @Input() event: EventData | undefined = undefined;
  venueIcon = faLocationDot
  calenderIcon = faCalendarAlt
}

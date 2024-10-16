import { Component, Input, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { faCalendarAlt, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { CarouselComponent } from '../carousel/carousel.component';

@Component({
  selector: 'event-description',
  standalone: true,
  imports: [FontAwesomeModule, CarouselComponent],
  templateUrl: './event-description.component.html',
  styleUrl: './event-description.component.css'
})
export class EventDescriptionComponent implements OnInit {
  @Input() event: EventData | undefined = undefined;
  images: string[] = [];
  venueIcon = faLocationDot
  calenderIcon = faCalendarAlt
  ngOnInit() {
    //create an array of input image
    if (this.event) {
      for (let i = 0; i < 3; i++) {
        this.images.push(this.event.bannerImg);
      }
    }
    console.log(this.images);
  }
}

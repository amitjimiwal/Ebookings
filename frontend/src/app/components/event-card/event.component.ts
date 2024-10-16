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
  imports: [RouterModule, FontAwesomeModule, CarouselComponent,CommonModule],
  templateUrl: './event.component.html',
  styleUrl: './event.component.css'
})
export class EventComponent implements OnInit {
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

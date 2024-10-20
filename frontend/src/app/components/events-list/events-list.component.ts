import { Component, inject, OnInit } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { EventService } from '../../services/event/event.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCalendarAlt, faLocationDot } from '@fortawesome/free-solid-svg-icons';
import { EventComponent } from '../event-card/event.component';
import { Category } from '../../models/interface/categories';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-events-list',
  standalone: true,
  imports: [CommonModule, RouterModule, EventComponent, FormsModule],
  templateUrl: './events-list.component.html',
  styleUrl: './events-list.component.css'
})
export class EventsListComponent implements OnInit {
  venueIcon = faLocationDot;
  calenderIcon = faCalendarAlt;
  public events: EventData[] = [];
  private eventService = inject(EventService);
  categories: Category[] = [
  ];
  sortType: string = '';
  selectedCategory: string = '';
  sortOrder: boolean = false;
  ngOnInit(): void {
    this.eventService.getCategories().subscribe(categories => this.categories = categories, (error) => {
      console.error("Error occurred while getting the data of categories", error);
    });
    this.updateEvents();
  }
  filterEvents(category: string): void {
    if (category === 'All') {
      this.selectedCategory = '';
    } else {
      this.selectedCategory = category;
    }
    this.updateEvents();
  }
  sortEvents(sortType: string): void {
    this.sortType = sortType;
    this.updateEvents();
  }
  onSortOrderChange(val: any): void {
    const selectedValue = val.target.value;
    this.sortOrder = selectedValue === 'highToLow' ? true : false;
    this.updateEvents();
  }
  updateEvents(): void {
    this.eventService.getEvents(this.selectedCategory, this.sortType, this.sortOrder).subscribe(events => this.events = events, (error) => {
      console.error("Error occurred while getting the data of events", error);
    }, () => {
      console.log("Events data fetched successfully");
    });
  }
}

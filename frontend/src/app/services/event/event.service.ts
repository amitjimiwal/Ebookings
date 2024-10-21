import { inject, Injectable } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../../models/interface/categories';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  http = inject(HttpClient);
  constructor() { }
  getEvents(category: string, sortType: string, sortOrder: boolean, searchQuery: string, searchTopic: string): Observable<EventData[]> {
    return this.http.get<EventData[]>(`http://localhost:5077/api/Events?SearchTopic=${searchTopic}&SearchQuery=${searchQuery}&Category=${category}&SortBy=${sortType}&IsDescending=${sortOrder}`);
  }
  getEventById(id: string): Observable<EventData> {
    return this.http.get<EventData>(`http://localhost:5077/api/Events/${id}`);
  }
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>('http://localhost:5077/api/Events/categories');
  }
}

import { inject, Injectable } from '@angular/core';
import { EventData } from '../../models/interface/event';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
export const dummyEventData: EventData[] = [
  {
    id: "e1c5e7e2-1234-4567-8901-123456789012",
    eventName: "Music Concert",
    date: new Date("2024-11-15"),
    eventTiming: "18:30:00",  // Format as "hh:mm:ss"
    createdAt: new Date("2024-09-24"),
    totalTickets: 500,
    description: "An electrifying music concert featuring top artists.",
    venue: "Open Air Auditorium",
    ticketPrice: 1500,
    availableTickets: 300,
    maxTicketsPerPerson: 5,
    bannerImg: "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/stand-up-comedy-event-banner-size-16%3A9-design-template-820a0329fa39e52c611f956b651f576d_screen.jpg?ts=1685775661",
    category: "Music"
  },
  {
    id: "a7b8c9d1-2345-6789-0123-456789abcdef",
    eventName: "Tech Conference 2024",
    date: new Date("2024-12-05"),
    eventTiming: "09:00:00",
    createdAt: new Date("2024-09-20"),
    totalTickets: 1000,
    description: "Annual tech conference discussing the latest in AI and cloud computing.",
    venue: "Tech Expo Center",
    ticketPrice: 3500,
    availableTickets: 850,
    maxTicketsPerPerson: 3,
    bannerImg: "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/stand-up-comedy-event-banner-size-16%3A9-design-template-820a0329fa39e52c611f956b651f576d_screen.jpg?ts=1685775661",
    category: "Conference"
  },
  {
    id: "f4e5d6a7-3456-7890-1234-567890abcdef",
    eventName: "Stand-up Comedy Night",
    date: new Date("2024-10-10"),
    eventTiming: "20:00:00",
    createdAt: new Date("2024-09-22"),
    totalTickets: 200,
    description: "A night full of laughter with renowned stand-up comedians.",
    venue: "City Hall Auditorium",
    ticketPrice: 500,
    availableTickets: 50,
    maxTicketsPerPerson: 4,
    bannerImg: "https://d1csarkz8obe9u.cloudfront.net/posterpreviews/stand-up-comedy-event-banner-size-16%3A9-design-template-820a0329fa39e52c611f956b651f576d_screen.jpg?ts=1685775661",
    category: "Comedy"
  },
  {
    id: "9b8c7d6e-4567-8901-2345-67890abcdef1",
    eventName: "Dil-Luminati Tour India - Diljit Dosanjh",
    date: new Date("2024-10-20"),
    eventTiming: "11:00:00",
    createdAt: new Date("2024-09-10"),
    totalTickets: 300,
    description: "Experience the magic of Diljit Dosanjh live in concert.",
    venue: "JLN Stadium , New Delhi",
    ticketPrice: 5000,
    availableTickets: 400,
    maxTicketsPerPerson: 3,
    bannerImg: "https://ts-production.imgix.net/images/mobile-cover-uploaded/23ed7b28-9a92-45fb-a37e-e1a2fc986455.jpg?auto=compress",
    category: "Entertainment"
  },
  {
    id: "abcd1234-5678-9101-2345-67890efghij1",
    eventName: "It was all a dream - Karan Aujla",
    date: new Date("2024-11-25"),
    eventTiming: "12:00:00",
    createdAt: new Date("2024-09-18"),
    totalTickets: 800,
    description: "Karan Aujla live in concert.",
    venue: "Wankhede Stadium, Mumbai",
    ticketPrice: 300,
    availableTickets: 600,
    maxTicketsPerPerson: 10,
    bannerImg: "https://storage.googleapis.com/loudest-news-photo/news-photo/17490.Untitled-design-(8).jpg",
    category: "Entertainment"
  }
];

@Injectable({
  providedIn: 'root'
})
export class EventService {
  http = inject(HttpClient);
  constructor() { }
  getEvents(): Observable<EventData[]> {
    return this.http.get<EventData[]>('http://localhost:5077/api/Events');
  }
  getEventById(id: string): Observable<EventData> {
    return this.http.get<EventData>(`http://localhost:5077/api/Events/${id}`);
  }
}

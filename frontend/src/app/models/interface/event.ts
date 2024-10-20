import { TicketDTO } from "./ticket";

export interface EventData {
     id: string;
     eventName: string;
     date: Date;                 // Date of the event
     eventTiming: string;         // Time as string, formatted like "hh:mm:ss"
     timeZone: string;            // Time zone of the event
     createdAt: Date;             // Event creation timestamp
     totalTickets: number;        // Total tickets available for the event
     description: string;         // Event description
     venueName: string;           // Name of the venue
     venueAddress: string;        // Address of the venue
     venueCapacity: number;       // Maximum capacity of the venue
     minPriceTicket: number;      // Minimum price of tickets for the event
     availableTickets: number;    // Number of tickets available
     maxTicketsPerPerson?: number; // Maximum tickets one person can purchase (optional)
     images?: string[];           // Array of image URLs for the event (optional)
     ticketTypes?: TicketDTO[];   // Array of ticket types (optional)
     category: string;            // Event category
}

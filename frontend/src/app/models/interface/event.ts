export interface EventData {
     id: string;
     eventName: string;
     date: Date;
     eventTiming: string;
     createdAt: Date;
     totalTickets?: number;
     description: string;
     venue: string;
     ticketPrice: number;
     availableTickets: number;
     maxTicketsPerPerson: number;
     bannerImg?: string | null;
     category: string;
}

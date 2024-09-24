export interface EventData {
     id: string;
     eventName: string;
     date: Date;                // Assuming you're handling date as a Date object in TS
     eventTiming: string;        // Time as string, formatted like "hh:mm:ss"
     createdAt: Date;            // Assuming this is also a Date object in TS
     totalTickets: number;
     description: string;
     venue: string;
     ticketPrice: number;
     availableTickets: number;
     maxTicketsPerPerson: number;
     bannerImg: string;
     category: string;
}

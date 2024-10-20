export interface TicketDTO {
     id: string;                 // Unique identifier for the ticket type
     eventID: string;            // ID of the event this ticket is associated with
     ticketPrice: number;        // Price of the ticket
     totalTickets: number;       // Total number of tickets available for this type
     availableTickets: number;   // Number of tickets still available
     displayName: string;        // Name of the ticket type (e.g., PREMIUM)
}

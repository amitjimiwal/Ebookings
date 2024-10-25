export interface EventBooking {
     eventName: string;
     eventLocation: string;
     eventDate: string; // ISO string format for date
     description: string;
     id: string; // Unique identifier for the booking
     name: string; // Name of the user booking the event
     email: string; // Email address of the user
     phoneNumber: number;
     eventId: string; // Unique identifier for the event
     noOfTickets: number; // Number of tickets booked
     bookedAt: string; // ISO string format for booking date and time
     totalPrice: number; // Total price for the tickets
     appUserID: string; // Unique identifier for the user in the app
     tickets: Ticket[]; // Array of tickets booked
     couponCodeApplied: string | null;
     discountPercentage: number;
}
interface Ticket {
     ticketTypeId: string;
     quantity: number;
     ticketName: string;
     totalPrice: number;
}

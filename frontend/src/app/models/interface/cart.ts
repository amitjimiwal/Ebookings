export interface Ticket {
  ticketId: string;
  ticketQuantity: number;
  singleticketPrice: number;
  ticketDisplayName: string;
}

export interface Cart {
  eventName: string;
  eventId: string;
  tickets: Ticket[];
  totalTickets: number;
  totalPrice: number;
  couponCode?: string; // Optional, in case there's no coupon applied
}
// excluding eventName and eventId from the Cart interface using Exclude
type CartWithoutEvent = Exclude<Cart, 'eventName'>;

export interface BookingRequest extends CartWithoutEvent {
  name: string;
  email: string;
  phoneNumber: string;
}
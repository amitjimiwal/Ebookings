import { Ticket } from "./cart";

export interface OrderSummary {
     name: string;
     email: string;
     phoneNumber: string;
     eventId: string;
     tickets: Ticket[];
     totalTickets: number;
     totalPrice: number;
     couponCode: string;
     discountAmount: number | null;
     finalAmount: number;
}
export interface PaymentDetails {
     cardNumber: string;
     cardHolderName: string;
     expiryMonth: string;
     expiryYear: string;
     cvv: string;
}

export interface PaymentPayload {
     checkoutID: string;
     paymentID: string;
     eventID: string;
     amountPaid: number;
     ticketsBought: number;
}
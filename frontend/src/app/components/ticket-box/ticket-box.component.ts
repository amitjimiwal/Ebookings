import { Component, Input, signal } from '@angular/core';
import { TicketDTO } from '../../models/interface/ticket';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ticket-box',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket-box.component.html',
  styleUrl: './ticket-box.component.css'
})
export class TicketBoxComponent {
  @Input() ticketData: TicketDTO[] | undefined = undefined;
  private ticketCounts = new Map<string, ReturnType<typeof signal<number>>>();
  constructor() {
    // Initialize signals for each ticket type
    this.ticketData?.forEach(ticket => {
      this.ticketCounts.set(ticket.id, signal(0));
    });
  }
  getTicketCount(ticketId: string) {
    return this.ticketCounts.get(ticketId)!;
  }
  incrementTicket(ticket: TicketDTO) {
    const currentCount = this.getTicketCount(ticket.id)();
    if (currentCount < ticket.availableTickets) {
      this.getTicketCount(ticket.id).set(currentCount + 1);
    }
  }
  decrementTicket(ticket: TicketDTO) {
    const currentCount = this.getTicketCount(ticket.id)();
    if (currentCount > 0) {
      this.getTicketCount(ticket.id).set(currentCount - 1);
    }
  }

  getTotalAmount(): number {
    if (!this.ticketData) return 0;
    return this.ticketData?.reduce((total, ticket) => {
      return total + (this.getTicketCount(ticket.id)() * ticket.ticketPrice);
    }, 0);
  }

  proceedToCheckout() {
    const selectedTickets = this.ticketData?.map(ticket => ({
      ticketType: ticket.displayName,
      quantity: this.getTicketCount(ticket.id)(),
      amount: this.getTicketCount(ticket.id)() * ticket.ticketPrice
    }));
    console.log('Proceeding to checkout with:', selectedTickets);
    // Add your checkout logic here
  }
}

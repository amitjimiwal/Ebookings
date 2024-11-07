import { Component, Input, OnInit, signal } from '@angular/core';
import { TicketDTO } from '../../models/interface/ticket';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart/cart.service';
import { EventData } from '../../models/interface/event';

@Component({
  selector: 'app-ticket-box',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket-box.component.html',
  styleUrl: './ticket-box.component.css'
})
export class TicketBoxComponent implements OnInit {
  @Input() ticketData: TicketDTO[] | undefined = undefined;
  @Input() event: EventData | undefined = undefined;
  cart$ = this.cartService.cart$;
  public isBookingAllowed = false;
  private ticketCounts = new Map<string, ReturnType<typeof signal<number>>>();
  constructor(private cartService: CartService) {
    // Initialize signals for each ticket type
    this.ticketData?.forEach(ticket => {
      this.ticketCounts.set(ticket.id, signal(0));
    });
  }
  ngOnInit(): void {
    this.cart$.subscribe(cart => {
      if (!cart || !this.event) {
        return;
      }
      this.isBookingAllowed = cart.eventId !== this.event.id;
    });
    console.log(this.isBookingAllowed);
  }
  updateTicketQuantity(ticketId: string, ticketInfo: Event) {
    const inputElement = ticketInfo.target as HTMLInputElement;
    const ticketData = this.ticketData?.filter(ticket => ticket.id === ticketId)[0];
    if (!ticketData || !this.event) {
      return;
    }
    const ticketDetails = {
      ticketId: ticketData.id,
      ticketQuantity: parseInt(inputElement.value),
      singleticketPrice: ticketData.ticketPrice,
      ticketDisplayName: ticketData.displayName
    }

    console.log(ticketDetails);
    this.cartService.addCartItems(this.event?.id, ticketDetails, this.event?.eventName);
  }
}

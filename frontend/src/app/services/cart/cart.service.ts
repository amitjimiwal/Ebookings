import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Cart, Ticket } from '../../models/interface/cart';
import { tick } from '@angular/core/testing';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private GST = 0;
  private cartSubject = new BehaviorSubject<Cart | null>(null);
  cart$ = this.cartSubject.asObservable();
  constructor() {
    this.loadCartFromStorage();
  }
  //load cart from local storage
  private loadCartFromStorage(): void {
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      this.cartSubject.next(JSON.parse(savedCart));
    }
  }

  addCartItems(eventId: string, ticket: Ticket, eventName: string): boolean {
    const currentCart = this.cartSubject.value;
    if (!currentCart) {
      console.log("Cart not found, creating new cart");
      const price = ticket.singleticketPrice * ticket.ticketQuantity;
      const cart: Cart = {
        eventId,
        eventName,
        tickets: new Array(ticket),
        totalPrice: (price) + (price * this.GST),
        totalTickets: ticket.ticketQuantity
      };
      this.updateCart(cart);
      this.saveCartToStorage(cart);
    } else {
      //check if the event in the cart is same as the event for which the ticket is being added
      if (currentCart.eventId !== eventId) {
        alert("You can only add tickets for a single event at a time. Please Remove the existing tickets to add tickets for a new event");
        return false;
      }
      this.addTickets(ticket);
    }
    return true;
  }
  private updateCart(cart: Cart): void {
    this.cartSubject.next(cart);
    this.saveCartToStorage(cart);
  }
  private saveCartToStorage(cart: Cart) {
    localStorage.setItem('cart', JSON.stringify(cart));
  }

  addTickets(ticket: Ticket): void {
    const currentCart = this.cartSubject.value;
    if (currentCart) {
      const ticketIndex = currentCart.tickets.findIndex(t => t.ticketId === ticket.ticketId);
      if (ticketIndex == -1) {
        console.log("Ticket not found, creating new ticket");
        currentCart.tickets.push(ticket);
      } else {
        console.log("Ticket found, updating ticket");
        if (ticket.ticketQuantity === 0) {
          currentCart.totalTickets -= currentCart.tickets[ticketIndex].ticketQuantity;
          const price = currentCart.tickets[ticketIndex].singleticketPrice * currentCart.tickets[ticketIndex].ticketQuantity;
          currentCart.totalPrice -= price + (price * this.GST);
          currentCart.tickets.splice(ticketIndex, 1);
          if (currentCart.tickets.length === 0) {
            this.deleteCart();
            return;
          }
          this.updateCart(currentCart);
          return;
        } else currentCart.tickets[ticketIndex].ticketQuantity = ticket.ticketQuantity;
      }
      currentCart.totalTickets = currentCart.tickets.reduce((acc, t) => acc + t.ticketQuantity, 0);
      //calculate total price with gst
      const price = currentCart.tickets.reduce((acc, t) => acc + (t.singleticketPrice * t.ticketQuantity), 0);
      currentCart.totalPrice = price + (price * this.GST);
      console.log("Cart updated with new ticket");
      console.log(currentCart);
      this.updateCart(currentCart);
    }
  }

  deleteCart(): void {
    localStorage.removeItem('cart');
    this.cartSubject.next(null);
  }
  deleteTicket(ticketId: string): void {
    const currentCart = this.cartSubject.value;
    if (currentCart) {
      const ticketIndex = currentCart.tickets.findIndex(t => t.ticketId === ticketId);
      if (ticketIndex !== -1) {
        currentCart.totalTickets -= currentCart.tickets[ticketIndex].ticketQuantity;
        const price = currentCart.tickets[ticketIndex].singleticketPrice * currentCart.tickets[ticketIndex].ticketQuantity;
        currentCart.totalPrice -= price + (price * this.GST);
        currentCart.tickets.splice(ticketIndex, 1);
        if (currentCart.tickets.length === 0) {
          this.deleteCart();
        }
      }
    }
  }
}

import { Component, Input } from '@angular/core';
import { Cart } from '../../models/interface/cart';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { CartService } from '../../services/cart/cart.service';
@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  @Input() cartData: Cart | null = null;
  constructor(private CartService: CartService, private router: Router) { }
  deleteCart() {
    this.CartService.deleteCart();
  }
  deleteItem(ticketId: string) {
    this.CartService.deleteTicket(ticketId);
  }
  proceedToCheckout() {
    if (this.cartData) {
      this.router.navigate(['/checkout']);
    }
  }
}

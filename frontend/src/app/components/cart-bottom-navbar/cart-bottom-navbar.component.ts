import { Component } from '@angular/core';
import { CartService } from '../../services/cart/cart.service';
import { Observable } from 'rxjs';
import { Cart } from '../../models/interface/cart';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-bottom-navbar',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './cart-bottom-navbar.component.html',
  styleUrl: './cart-bottom-navbar.component.css'
})
export class CartBottomNavbarComponent {
  cart: Cart | undefined;
  constructor(private cartService: CartService) {
    this.cartService.cart$.subscribe(cart => {
      if (cart) {
        this.cart = cart;
      }
    });
  }
}


import { Component } from '@angular/core';
import { CartItemComponent } from '../cart-item/cart-item.component';
import { Cart } from '../../models/interface/cart';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService } from '../../services/cart/cart.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CartItemComponent, CommonModule, RouterModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cart$ = this.cartService.cart$;
  constructor(private cartService: CartService) {
    this.cartService.cart$.subscribe(cart => {
      this.cartData = cart;
    });
  }
  cartData: Cart | null = null;
}

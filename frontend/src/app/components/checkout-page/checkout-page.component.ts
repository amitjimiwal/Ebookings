import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserInfoService } from '../../services/user-info-service/user-info-service.service';
import { CheckoutService } from '../../services/checkout/checkout.service';
import { CartService } from '../../services/cart/cart.service';
import { BookingRequest, Cart } from '../../models/interface/cart';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-checkout-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './checkout-page.component.html',
  styleUrl: './checkout-page.component.css'
})
export class CheckoutPageComponent implements OnInit {
  isProcessing: boolean = false;
  checkoutForm: FormGroup;
  originalPrice: number = 0;
  discountPercent: number = 0;
  finalPrice: number = this.originalPrice;
  couponError: boolean = false;
  couponSuccess: boolean = false;
  cart: Cart | null = null;
  constructor(private formBuilder: FormBuilder, private userInformationService: UserInfoService, private checkoutService: CheckoutService, private cartService: CartService, private route: ActivatedRoute, private router: Router) {
    this.checkoutForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      phoneNumber: ['', [Validators.pattern('^[0-9]*$'), Validators.maxLength(10), Validators.minLength(10), Validators.required]],
      couponCode: ['']
    });
    this.cart = this.cartService.getCart();
    this.originalPrice = this.cart?.totalPrice || 0;
    this.finalPrice = this.originalPrice;
    this.checkoutForm.get('couponCode')?.valueChanges.subscribe(() => {
      this.couponError = false;
      this.couponSuccess = false;
    });
  }
  ngOnInit(): void {
    //TODO: get all couponCode for the event
    const user = this.userInformationService.getUser();
    if (user) {
      this.checkoutForm.patchValue({
        name: user.userName,
        email: user.email,
        phoneNumber: user.phoneNumber
      });
    }
  }

  proceedToPayment() {
    const payload: BookingRequest = {
      ...this.checkoutForm.value,
      ...this.cart,
      couponCode: !this.couponError && this.couponSuccess ? this.checkoutForm.get('couponCode')?.value : null
    }
    this.isProcessing = true;
    this.checkoutService.createCheckoutSession(payload).subscribe((response: any) => {
      this.isProcessing = false;
      //navigate to checkout page
      //clear the cart
      this.cartService.deleteCart();
      this.router.navigate([`checkout-payment/${response.checkoutID}/${response.paymentID}`]);
    }, (error) => {
      this.isProcessing = false;
      alert("Error while creating checkout session");
    });
  }
  applyCouponCode() {
    const couponCode = this.checkoutForm.get('couponCode')?.value;
    const eventID = this.cartService.getCart()?.eventId;
    if (couponCode && eventID) {
      this.checkoutService.applyCoupon(couponCode, eventID).subscribe((response: any) => {
        this.couponSuccess = true;
        this.couponError = false;
        this.discountPercent = response.discountPercentage;
        this.finalPrice = this.originalPrice - (this.originalPrice * this.discountPercent / 100);
      }, (error) => {
        this.couponError = true;
      });
    }
  }
}

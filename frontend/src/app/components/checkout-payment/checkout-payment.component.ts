import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { OrderSummary, PaymentDetails, PaymentPayload } from '../../models/interface/payment';
import { CommonModule } from '@angular/common';
import { CheckoutService } from '../../services/checkout/checkout.service';
import { PaymentService } from '../../services/payment/payment.service';

@Component({
  selector: 'app-checkout-payment',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterModule],
  templateUrl: './checkout-payment.component.html',
  styleUrl: './checkout-payment.component.css'
})
export class CheckoutPaymentComponent {
  orderData: OrderSummary | undefined = undefined
  paymentForm: FormGroup;
  checkoutID: string | null = null;
  paymentID: string | null = null;
  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router, private checkoutService: CheckoutService, private paymentService: PaymentService) {
    this.paymentForm = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.pattern('^[0-9]{16}$')]],
      cardHolderName: ['', Validators.required],
      expiryMonth: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])$')]],
      expiryYear: ['', [Validators.required, Validators.pattern('^[0-9]{4}$')]],
      cvv: ['', [Validators.required, Validators.pattern('^[0-9]{3,4}$')]]
    });
  }
  ngOnInit(): void {
    this.checkoutID = this.route.snapshot.params['checkoutID'];
    this.paymentID = this.route.snapshot.params['paymentID'];
    if (this.checkoutID)
      this.checkoutService.getCheckoutSession(this.checkoutID).subscribe((res: OrderSummary) => {
        this.orderData = res
      }, (err) => {
        alert(err.error);
        console.log("Error in getting the checkout details");
      })
  }

  onSubmit() {
    if (this.paymentForm.valid) {
      const paymentDetails: PaymentPayload = {
        eventID: this.orderData?.eventId || '',
        checkoutID: this.checkoutID || '',
        ticketsBought: this.orderData?.totalTickets || 0,
        amountPaid: this.orderData?.finalAmount || 0,
        paymentID: this.paymentID || ''
      }
      console.log('Payment Details:', paymentDetails);
      // Handle payment processing here
      this.paymentService.createPayment(paymentDetails).subscribe((res: any) => {
        this.router.navigate([`/success/${res.transactionID}`]);
      }, (err) => {
        alert(err.error);
        console.log('Error while processing payment', err);
      });
    }
  }
}

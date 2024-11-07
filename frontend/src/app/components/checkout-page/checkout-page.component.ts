import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserInfoService } from '../../services/user-info-service/user-info-service.service';

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
  originalPrice: number = 99.99;
  discountPercent: number = 0;
  finalPrice: number = this.originalPrice;
  couponError: boolean = false;
  couponSuccess: boolean = false;
  constructor(private formBuilder: FormBuilder, private userInformationService: UserInfoService) {
    const user = userInformationService.getUser()
    this.checkoutForm = formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]],
      phoneNumber: ['', Validators.required, Validators.minLength(10), Validators.maxLength(10)],
      couponCode: ['']
    });
  }
  ngOnInit(): void {
    //get all couponCode for the event
  }
  async applyCoupon() {
    const couponCode = this.checkoutForm.get('couponCode')?.value.toUpperCase();
    if (!couponCode) {
      this.couponError = true;
      this.couponSuccess = false;
      return;
    } else {

    }
  }
  onSubmit() {
    console.log(this.checkoutForm.value);
  }
}

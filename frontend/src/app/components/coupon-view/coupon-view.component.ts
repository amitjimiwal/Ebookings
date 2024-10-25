import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-coupon-view',
  standalone: true,
  imports: [],
  templateUrl: './coupon-view.component.html',
  styleUrl: './coupon-view.component.css'
})
export class CouponViewComponent {
  @Input() couponCode: string = '';
  @Input() couponDiscount: number = 0;
}

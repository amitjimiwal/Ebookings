import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BookingRequest } from '../../models/interface/cart';
import { TokenService } from '../token-service/token-service.service';
import { Observable } from 'rxjs';
import { OrderSummary } from '../../models/interface/payment';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  http = inject(HttpClient);
  constructor(private tokenStorage: TokenService) { }

  createCheckoutSession(payload: BookingRequest) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.post('http://localhost:5077/api/Checkout', payload, { headers });
  }
  getCheckoutSession(sessionId: string): Observable<OrderSummary> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.get<OrderSummary>(`http://localhost:5077/api/Checkout/Session/${sessionId}`, { headers });
  }

  applyCoupon(couponCode: string, eventId: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.get(`http://localhost:5077/CouponCode?Code=${couponCode}&EventID=${eventId}`, { headers });
  }
}

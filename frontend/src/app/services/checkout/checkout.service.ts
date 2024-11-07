import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BookingRequest } from '../../models/interface/cart';
import { TokenService } from '../token-service/token-service.service';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  http = inject(HttpClient);
  constructor(private tokenStorage: TokenService) {}

  createCheckoutSession(payload: BookingRequest) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.post('http://localhost:5077/api/Checkout', { headers, payload });
  }
}

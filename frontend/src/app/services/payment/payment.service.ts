import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { TokenService } from '../token-service/token-service.service';
import { PaymentPayload } from '../../models/interface/payment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  http = inject(HttpClient);
  constructor(private tokenStorage: TokenService) { }

  createPayment(payload: PaymentPayload): Observable<PaymentPayload> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.post<PaymentPayload>('http://localhost:5077/api/Checkout/Booking', payload, { headers });
  }
}

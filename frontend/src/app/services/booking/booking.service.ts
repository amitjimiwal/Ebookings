import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenService } from '../token-service/token-service.service';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  http = inject(HttpClient);
  constructor(private tokenStorage: TokenService) { }

  postBooking(formdata: any): Observable<any> {
    // add authorization header with jwt token
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.http.post('http://localhost:5077/api/Bookings', formdata, { headers });
  }
}

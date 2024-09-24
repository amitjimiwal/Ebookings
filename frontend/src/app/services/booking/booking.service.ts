import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookingService {
  http = inject(HttpClient);
  constructor() { }

  postBooking(formdata: any):Observable<any> {
    return this.http.post('http://localhost:5077/api/Bookings', formdata);
  }
}

import { Injectable } from '@angular/core';
import { TokenService } from '../token-service/token-service.service';
import { HttpClient, HttpHeaderResponse, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { UpdateUserDto } from '../../models/interface/auth';

@Injectable({
  providedIn: 'root'
})
export class UserInfoService {
  private readonly USER_TOKEN_NAME = 'user_info';
  constructor(private tokenStorage: TokenService, private http: HttpClient) { }
  //setUser
  setUser(user: any): void {
    localStorage.setItem(this.USER_TOKEN_NAME, JSON.stringify(user));
  }

  //getUser
  getUser(): any {
    const data = localStorage.getItem(this.USER_TOKEN_NAME);
    return data ? JSON.parse(data) : null;
  }

  //Remove_USer

  removeUser(): void {
    localStorage.removeItem(this.USER_TOKEN_NAME);
  }

  updateUser(user: UpdateUserDto): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    })

    //pipe is used here to intercept the response and do some operation on it
    return this.http.put('http://localhost:5077/api/Auth/update', user, { headers }).pipe(map((data: any) => {
      this.setUser(data.user);
    }));
  }
}

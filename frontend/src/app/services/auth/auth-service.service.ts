import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TokenService } from '../token-service/token-service.service';
import { UserInfoService } from '../user-info-service/user-info-service.service';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AppUser } from '../../models/interface/user';
import { LoginDto, RegisterDto } from '../../models/interface/auth';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private UserSubject: BehaviorSubject<AppUser | null>;
  public currentUser: Observable<AppUser | null>;
  constructor(private httpClient: HttpClient, private tokenStorage: TokenService, private userStorage: UserInfoService, private router: Router) {
    //kind of global state to hold user data
    this.UserSubject = new BehaviorSubject(this.userStorage.getUser());

    //getting the state as observable so that every component can access/subscribe to it
    this.currentUser = this.UserSubject.asObservable();
  }
  //login
  login(UserPayload: LoginDto): Observable<AppUser> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    });
    return this.httpClient.post('http://localhost:5077/api/Auth/login', UserPayload, { headers }).pipe(map((data: any) => {
      this.userStorage.setUser(data.user);
      this.tokenStorage.setToken(data.token);
      return data.user;
    }));
  }

  //logout
  logout(): void {
    this.tokenStorage.removeToken();
    this.userStorage.removeUser();
    this.UserSubject.next(null);
    this.router.navigate(['/login']);
  }

  //signup
  signUp(UserPayload: RegisterDto): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.tokenStorage.getToken()}`
    })
    return this.httpClient.post('http://localhost:5077/api/Auth/register', UserPayload, { headers, responseType: 'text' });
  }

  //isAuthenticated
  isAuthenticated(): boolean {
    return this.tokenStorage.getToken() ? true : false;
  }
}

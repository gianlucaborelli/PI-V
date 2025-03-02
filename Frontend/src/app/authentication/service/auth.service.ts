import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RegisterModel } from '../models/register.model';
import { RefreshTokenModel } from '../models/refresh-token.model';
import { LoginModel } from '../models/login.model';
import { environment } from '../../../environments/environment';
import { UserStoreService } from './user-store.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  baseUrl: String = environment.baseUrl;
  private userPayload: any;

  constructor(
    private http: HttpClient,
    private router: Router,
    private userStore: UserStoreService) {
    this.userPayload = this.decodedToken();
  }

  register(userObj: RegisterModel) {
    const url = `${this.baseUrl}/api/users/account/register`;
    return this.http.post<any>(url, userObj);
  }

  login(loginObj: LoginModel) {
    localStorage.clear();
    const url = `${this.baseUrl}/api/users/account/login`;
    return this.http.post<any>(url, loginObj);
  }

  renewToken(tokenApi: RefreshTokenModel) {
    var http = this.http.post<any>(`${this.baseUrl}/auth/refresh-token`, tokenApi, httpOptions)
    return http;
  }

  registryOnLocalStorage(log: any) {
    this.storeToken(log.accessToken!);
    this.storeRefreshToken(log.refreshToken!);
    let decodedValue = this.decodedToken();
    this.userStore.storeFullName(decodedValue.unique_name);
    this.userStore.storeEmail(decodedValue.email);
    this.userStore.storeRole(decodedValue.role);
    this.userStore.storeId(decodedValue.nameid);
  }

  logout() {
    localStorage.clear();
    this.router.navigate(['login'])
  }

  storeToken(tokenValue: string) {
    localStorage.setItem('token', tokenValue)
  }

  storeRefreshToken(tokenValue: string) {
    localStorage.setItem('refreshToken', tokenValue)
  }

  getToken() {
    return localStorage.getItem('token')
  }

  getRefreshToken() {
    return localStorage.getItem('refreshToken')
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token')
  }

  decodedToken() {
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    return jwtHelper.decodeToken(token);
  }

  getfullNameFromToken() {
    if (this.userPayload)
      return this.userPayload.name;
  }

  getRoleFromToken() {
    if (this.userPayload)
      return this.userPayload.role;
  }
}


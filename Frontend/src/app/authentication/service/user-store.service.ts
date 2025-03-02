import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserStoreService {

  public getFullName() {
    return localStorage.getItem('userName');
  }

  public storeFullName(fullname: string) {
    localStorage.setItem('userName', fullname);
  }

  public getEmail() {
    return localStorage.getItem('userEmail');
  }

  public storeEmail(email: string) {
    localStorage.setItem('userEmail', email);
  }

  public getRole() {
    return localStorage.getItem('role');
  }

  public storeRole(role: string) {
    localStorage.setItem('role', role);
  }

  public getId() {
    return localStorage.getItem('id');
  }

  public storeId(role: string) {
    localStorage.setItem('id', role);
  }
}

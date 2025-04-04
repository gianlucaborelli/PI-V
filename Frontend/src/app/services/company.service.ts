import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private API_URL = `${environment.baseUrl}/api/system-manager/company`; // Ajuste conforme o endpoint do backend

  constructor(private http: HttpClient) {}

  registerCompany(companyData: any): Observable<any> {
    return this.http.post<any>(this.API_URL, companyData);
  }

  
}

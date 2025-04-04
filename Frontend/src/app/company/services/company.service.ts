import { Injectable } from '@angular/core';
import { CompanyModel } from '../models/company.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private API_URL = `${environment.baseUrl}/api/system-manager/company`;
  constructor(private http: HttpClient) { }

  getAllCompanies(): Observable<CompanyModel[]> {
    let result = this.http.get<CompanyModel[]>(this.API_URL);
    return result
  }
}

import { Injectable } from '@angular/core';
import { CompanyModel } from '../models/company.model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, map, Observable, of } from 'rxjs';
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

  getCompanyDetail(companyId: string): Observable<CompanyModel> {
    let result = this.http.get<CompanyModel>(this.API_URL + '/' + companyId);
    return result
  }

  registerNewCompany(company: CompanyModel): Observable<CompanyModel> {
    let result = this.http.post<CompanyModel>(this.API_URL, company);
    return result
  }

  updateCompany(company: CompanyModel): Observable<CompanyModel> {
    let result = this.http.put<CompanyModel>(this.API_URL, company);
    return result
  }

  deleteCompany(company: string): Observable<boolean> {
    return this.http.delete<void>(`${this.API_URL}/${company}`, { observe: 'response' })
      .pipe(
        map(response => response.status === 200),
        catchError(() => of(false))
      );
  }
}

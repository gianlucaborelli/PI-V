import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {
  private API_URL = `${environment.baseUrl}/api/system-manager/company/{companyId}/module`;

  constructor(private http: HttpClient) {}

  registerModule(companyId: string, moduleData: any): Observable<any> {
    const url = `${environment.baseUrl}/api/system-manager/company/${companyId}/module`;
    return this.http.post<any>(url, moduleData);
  }
}

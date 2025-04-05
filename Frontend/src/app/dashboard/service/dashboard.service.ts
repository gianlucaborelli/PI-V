import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { DashboardModel } from '../models/dashboard.model';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private API_URL = `${environment.baseUrl}/dashboard`;
  constructor(private http: HttpClient) { }

  getDashboardData(moduleId: string, dateTime?: Date): Observable<DashboardModel> {
    let params = new HttpParams().set('moduleId', moduleId);

    if (dateTime) {
      params = params.set('dateTime', dateTime.toDateString());
    }

    let result = this.http.get<DashboardModel>(this.API_URL, { params });
    return result
  }
}

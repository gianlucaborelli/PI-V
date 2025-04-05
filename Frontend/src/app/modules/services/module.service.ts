import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs';
import { ModuleModel } from '../models/module.model';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {

  private API_URL = `${environment.baseUrl}/api/system-manager/company/`;
  constructor(private http: HttpClient) { }

  getAllModules(companyId: string): Observable<ModuleModel[]> {
    let result = this.http.get<ModuleModel[]>(this.API_URL + companyId + '/module');
    return result
  }
}

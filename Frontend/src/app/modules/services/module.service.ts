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

  insertModule(companyId: string, module: ModuleModel): Observable<ModuleModel> {
    let result = this.http.post<ModuleModel>(this.API_URL + companyId + '/module', module);
    return result
  }

  updateModule(companyId: string, module: ModuleModel): Observable<ModuleModel> {
    let result = this.http.put<ModuleModel>(this.API_URL + companyId + '/module', module);
    return result
  }

  regenerateModuleToken(companyId: string, moduleId: string): Observable<boolean> {
    let result = this.http.post<boolean>(this.API_URL + companyId + '/module/' + moduleId + '/generate-access-token', null);
    return result
  }

  deleteModule(companyId: string, moduleId: string): Observable<void> {
    let result = this.http.delete<void>(this.API_URL + companyId + '/module/' + moduleId );
    return result
  }
}



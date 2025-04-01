import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'; // Ajuste o caminho se necessário

@Injectable({
  providedIn: 'root'
})
export class SensoresService {
  private API_URL = `${environment.baseUrl}/api/sensores`;

  constructor(private http: HttpClient) {}

  getSensores(): Observable<any> {
    return this.http.get<any>(this.API_URL);
  }
}

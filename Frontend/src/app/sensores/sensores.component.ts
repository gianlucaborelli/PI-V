import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SensoresService {
  private API_URL = 'https://localhost:5001/api/sensores'; // Ajuste conforme o backend

  constructor(private http: HttpClient) {}

  getSensores(): Observable<any> {
    return this.http.get<any>(this.API_URL);
  }
}

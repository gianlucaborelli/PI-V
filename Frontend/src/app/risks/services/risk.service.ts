import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria, RiskModel } from '../models/risk.model';

@Injectable({
  providedIn: 'root'
})
export class RiskService {

  private API_URL = `${environment.baseUrl}/api/system-manager/risk`;
  constructor(private http: HttpClient) { }

  categorias = signal<Categoria[]>([]);

  // https://pi-v.onrender.com/api/system-manager/risk?riskType=TermoRisk'
  loadModules(riskType: string) {
    const params = new HttpParams().set('riskType', riskType);
    this.http.get<RiskModel[]>(this.API_URL, { params }).subscribe(data => {
      this.categorias.set(this.agruparDados(data));
    });
  }

  private agruparDados(data: RiskModel[]): Categoria[] {
    const map = new Map<string, Categoria>();
    for (const item of data) {
      if (!map.has(item.category)) {
        map.set(item.category, { nome: item.category, subcategorias: [] });
      }
      const cat = map.get(item.category)!;
      let sub = cat.subcategorias.find(s => s.nome === item.subCategory);
      if (!sub) {
        sub = { nome: item.subCategory, itens: [] };
        cat.subcategorias.push(sub);
      }
      sub.itens.push({
        id: item.id,
        nome: item.activity,
        metabolicRate: item.metabolicRate
      });
    }
    return Array.from(map.values());
  }
}

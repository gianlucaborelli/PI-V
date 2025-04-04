import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SensoresService } from '../services/sensores.service'; 

@Component({
  selector: 'app-historico',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './historico.component.html',
  styleUrls: ['./historico.component.css']
})
export class HistoricoComponent implements OnInit {
  sensores: any[] = [];

  constructor(private sensoresService: SensoresService) {}

  ngOnInit(): void {
    this.sensoresService.getSensores().subscribe({
      next: (data: any) => this.sensores = data,
      error: (err: any) => console.error('Erro ao carregar sensores:', err)
    });
  }
}

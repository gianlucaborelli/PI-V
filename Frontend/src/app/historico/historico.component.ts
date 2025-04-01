import { Component, OnInit } from '@angular/core';
import { SensoresService } from 'src/app/services/sensores.service';

@Component({
  selector: 'app-historico',
  standalone: true,
  templateUrl: './historico.component.html',
  styleUrls: ['./historico.component.css']
})
export class HistoricoComponent implements OnInit {
  sensores: any[] = [];

  constructor(private sensoresService: SensoresService) {}

  ngOnInit(): void {
    this.sensoresService.getSensores().subscribe({
      next: (data) => this.sensores = data,
      error: (err) => console.error('Erro ao carregar sensores:', err)
    });
  }
}

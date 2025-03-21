import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgClass } from '@angular/common';
import { SensoresService } from '../services/sensores.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, NgClass],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  sensores: any[] = [];

  constructor(private sensoresService: SensoresService) {}

  ngOnInit(): void {
    this.sensoresService.getSensores().subscribe(data => {
      this.sensores = data;
    });
  }

  getStatus(temp: number, umid: number): string {
    if (temp > 30 || umid < 40) return 'Insalubre';
    if (temp > 28) return 'Alerta';
    return 'Normal';
  }

  getStatusClass(sensor: any): string {
    const status = this.getStatus(sensor?.temperatura, sensor?.umidade);
    switch (status) {
      case 'Normal': return 'status-normal';
      case 'Alerta': return 'status-alerta';
      case 'Insalubre': return 'status-insalubre';
      default: return '';
    }
  }
}

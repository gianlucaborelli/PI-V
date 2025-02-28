import { Component, OnInit } from '@angular/core';
import { SensoresService } from '../services/sensores.service';

@Component({
  selector: 'app-dashboard',
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
}

import { Component, Input } from '@angular/core';
import { LocationSummaryModel } from '../../models/sensors.model';
import { ChartDataPipe } from "../../pipes/chartDataPipe";
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';
import { Color, LegendPosition, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';

@Component({
  selector: 'app-summary-tile',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    NgxChartsModule,
    ChartDataPipe],
  templateUrl: './summary-tile.component.html',
  styleUrl: './summary-tile.component.css'
})
export class SummaryTileComponent {
  @Input() locationSummary: LocationSummaryModel | undefined;

  eixoY: number[] = [];
  ngOnInit(): void {
    if (!this.locationSummary?.series?.length) return;

    const maxValor = Math.max(...this.locationSummary.series.map(item => item.humidity || 0));
    const maxAjustado = Math.ceil(maxValor / 20) * 20;
    const passo = 20;

    for (let i = 0; i <= maxAjustado; i += passo) {
      this.eixoY.push(i);
    }

    this.yAxisTicks = this.eixoY;
  }

  // chart options
  legend: boolean = false;
  legendPosition: LegendPosition = LegendPosition.Below;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Horário';
  yAxisLabel: string = '°C / %';
  yAxisTicks = this.eixoY;
  timeline: boolean = true;
  colorScheme: Color = {
    name: 'customScheme',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };


  formatXAxisLabel = (value: string | Date): string => {
    const date = new Date(value);
    return `${date.getHours()}:${String(date.getMinutes()).padStart(2, '0')}`;
  };
}

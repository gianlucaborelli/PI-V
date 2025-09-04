import { Pipe, PipeTransform } from '@angular/core';
import { SeriesDataModel } from '../models/sensors.model';

@Pipe({ name: 'chartData' })
export class ChartDataPipe implements PipeTransform {
  transform(data: SeriesDataModel[] | null | undefined): any[] {
    if (!data || data.length === 0) return [];

    return [
      {
        name: 'Humidity',
        series: data.map(d => ({
          name: d.createdAt ?? '', // se não tiver data, evita quebrar
          value: d.humidity ?? 0   // se null/undefined, cai para 0
        }))
      },
      {
        name: 'Dry Bulb Temperature',
        series: data.map(d => ({
          name: d.createdAt ?? '',
          value: d.dryBulbTemperature ?? 0
        }))
      },
      {
        name: 'Dark Bulb Temperature',
        series: data.map(d => ({
          name: d.createdAt ?? '',
          value: d.darkBulbTemperature ?? 0
        }))
      }
    ];
  }
}

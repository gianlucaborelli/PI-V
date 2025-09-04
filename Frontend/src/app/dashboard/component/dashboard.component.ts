import { Component, OnInit } from '@angular/core';
import { CommonModule, registerLocaleData } from '@angular/common';
import { DashboardService } from '../service/dashboard.service';
import { DashboardModel } from '../models/dashboard.model';
import { Color, NgxChartsModule, ScaleType } from '@swimlane/ngx-charts';
import { CompanyService } from '../../company/services/company.service';
import { CompanyModel } from '../../company/models/company.model';
import { ModuleModel } from '../../modules/models/module.model';
import { ModuleService } from '../../modules/services/module.service';
import { MATERIAL_MODULES } from '../../shared/imports/material.imports';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { MatSelectChange } from '@angular/material/select';
import localePt from '@angular/common/locales/pt';
import { LOCALE_ID } from '@angular/core';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import {
  MomentDateAdapter,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS
} from '@angular/material-moment-adapter';
import { MY_FORMATS } from '../../shared/constants/MY_FORMATS ';
import * as _moment from 'moment';
import 'moment/locale/pt-br';
import { LocationSummaryModel } from '../models/sensors.model';
import { ChartDataPipe } from "../pipes/chartDataPipe";

registerLocaleData(localePt);
const moment = _moment;
moment.locale('pt-br');

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    CommonModule,
    NgxChartsModule,
    ChartDataPipe
],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: MAT_DATE_LOCALE, useValue: 'pt-BR' },
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS] },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS }
  ]
})
export class DashboardComponent implements OnInit {
  dashboardData: DashboardModel = {
    locationsSummary: []
  };
  processado: any;
  companies: CompanyModel[] = [];
  companySelected!: string;

  modules: ModuleModel[] = [];
  modulesSelected!: string;

  dateSelected!: Date | null;

  // chart options
  view: [number, number] = [880, 400];
  legend: boolean = true;
  showLabels: boolean = true;
  animations: boolean = true;
  xAxis: boolean = true;
  yAxis: boolean = true;
  showYAxisLabel: boolean = true;
  showXAxisLabel: boolean = true;
  xAxisLabel: string = 'Horário';
  yAxisLabel: string = 'C';
  timeline: boolean = true;
  colorScheme: Color = {
    name: 'customScheme',
    selectable: true,
    group: ScaleType.Ordinal,
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };

  constructor(private dashboardService: DashboardService, private companyService: CompanyService, private moduleService: ModuleService) { }

  ngOnInit(): void {
    this.companyService.getAllCompanies().subscribe({
      next: (data: CompanyModel[]) => {
        this.companies = data;
      },
      error: (err: any) => console.error('Erro ao carregar Companias:', err)
    });
  }

  formatXAxisLabel = (value: string | Date): string => {
    const date = new Date(value);
    return `${date.getHours()}:${String(date.getMinutes()).padStart(2, '0')}`;
  };

  onCompanySelected(event: MatSelectChange) {
    const selectedId = event.value;

    this.moduleService.getAllModules(selectedId).subscribe({
      next: (data: ModuleModel[]) => {
        this.modules = data;
      },
      error: (err: any) => console.error('Erro ao carregar Modulos:', err)
    });
  }

  onModuleSelected(event: MatSelectChange) {
    const selectedId = event.value;

    this.dashboardService.getDashboardData(selectedId).subscribe({
      next: (data: DashboardModel) => {
        this.dashboardData = data;
        console.log(data);
      },
      error: (err: any) => {
        console.error('Erro ao carregar sensores:', err);
        this.dashboardData = {
          locationsSummary: []
        };
      }
    });

    console.log(this.dashboardData);
  }

  addEvent(event: MatDatepickerInputEvent<Date>) {
    if (event.value !== null) {
      const selectedDate = (event.value as unknown as moment.Moment).toDate(); // conversão explícita

      this.dateSelected = selectedDate;
      this.dashboardService.getDashboardData(this.modulesSelected, selectedDate).subscribe({
        next: (data: DashboardModel) => {
          this.dashboardData = data;
        },
        error: (err: any) => {
          console.error('Erro ao carregar sensores:', err);
          this.dashboardData = {
            locationsSummary: []
          };
        }
      });
    }
  }

  transformToChartData(data: any[]): any[] {
    return [
      {
        name: 'Humidity',
        series: data.map(d => ({
          name: d.createdAt,
          value: d.humidity
        }))
      },
      {
        name: 'Dry Bulb Temperature',
        series: data.map(d => ({
          name: d.createdAt,
          value: d.dryBulbTemperature
        }))
      },
      {
        name: 'Dark Bulb Temperature',
        series: data.map(d => ({
          name: d.createdAt,
          value: d.darkBulbTemperature
        }))
      }
    ];
  }
}

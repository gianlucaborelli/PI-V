import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
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

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    CommonModule,
    NgxChartsModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  dashboardData: DashboardModel = {
    ibtgEstimation: 0,
    series: []
  };

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
      },
      error: (err: any) => {
        console.error('Erro ao carregar sensores:', err);
        this.dashboardData = {
          ibtgEstimation: 0,
          series: []
        };
      }
    });
  }

  addEvent(event: MatDatepickerInputEvent<Date>) {
    if (event.value !== null) {
      this.dateSelected = event.value;
      this.dashboardService.getDashboardData(this.modulesSelected, event.value).subscribe({
        next: (data: DashboardModel) => {
          this.dashboardData = data;
        },
        error: (err: any) => {
          console.error('Erro ao carregar sensores:', err);
          this.dashboardData = {
            ibtgEstimation: 0,
            series: []
          };
        }
      });
    }
  }
}

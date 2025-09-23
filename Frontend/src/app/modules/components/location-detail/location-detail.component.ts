import { Component, EventEmitter, Input, Output, Signal, signal } from '@angular/core';
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';
import { RisksTypeEnum } from '../../../risks/RisksTypesEnum';
import { LocationModel, RiskLimitsModel } from '../../models/location.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { Categoria } from '../../../risks/models/risk.model';
import { RiskService } from '../../../risks/services/risk.service';

@Component({
  selector: 'app-location-detail',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    CommonModule,
    FormsModule
  ],
  templateUrl: './location-detail.component.html',
  styleUrl: './location-detail.component.css'
})
export class LocationDetailComponent {
  @Input({ required: true }) location!: LocationModel;
  @Output() locationChange = new EventEmitter<LocationModel>();

  categorias$: Observable<Categoria[]> = of([]);
  readonly riskTypeNum = RisksTypeEnum;
  riskTypeList: string[];

  selectedType: string = '';
  selectedRisk: string = '';

  constructor(
    public riskService: RiskService
  ) {
    this.riskTypeList = Object.values(this.riskTypeNum);
  }

  updateName(newName: string) {
    this.locationChange.emit({ ...this.location, name: newName });
  }

  updateRiskType(newType: string) {
    this.riskService.loadModules(newType);
    this.location.riskLimits
  }

  onRiskTypeChange(risk: RiskLimitsModel, newType: string) {
    risk.type = newType;
    this.riskService.loadModules(newType);
    console.log('Tipo atualizado:', risk);
  }

  onRiskActivityChange(risk: RiskLimitsModel, newRiskId: string) {
    risk.riskId = newRiskId;
    console.log('Atividade selecionada:', risk);
  }
}

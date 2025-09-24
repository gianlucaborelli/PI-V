import { Component, inject, Input, signal } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MATERIAL_MODULES } from '../../shared/imports/material.imports';
import { MatDialogRef } from '@angular/material/dialog';
import { ModuleService } from '../services/module.service';
import { ModuleModel } from '../models/module.model';
import { LocationModel, RisksTypeEnum } from '../models/location.model';

@Component({
  selector: 'app-module-detail',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    FormsModule,
    ReactiveFormsModule],
  templateUrl: './module-detail.component.html',
  styleUrls: ['./module-detail.component.css']
})
export class ModuleDetailComponent {
  readonly dialogRef = inject(MatDialogRef<ModuleDetailComponent>);
  @Input() tags: string[] | undefined;
  @Input() companyId: string | undefined;
  @Input() readonly module = signal<ModuleModel>({
    id: '',
    name: '',
    locations: []
  });
  readonly locations = signal<Array<LocationModel>>([]);

  selectedTag: string | undefined;
  selectedType: string | undefined;
  readonly riskTypeNum = RisksTypeEnum ;
  riskTypeList: string[] ;

  constructor(
    private moduleService: ModuleService,
  ) {
    this.dialogRef.disableClose = true;
    this.dialogRef.updateSize("60%");
    this.riskTypeList = Object.values(this.riskTypeNum);
    console.log(this.riskTypeList)
  }

  addSensor() {
    this.module.update(m => ({
      ...m,
      locations: [
        ...(m.locations ?? []),
        { type: this.selectedType,
          name: "",
          description: "",
          moduleId: this.module().id,
          riskLimits: [{
            riskId: "",
            type: ""
          }],

         } as LocationModel
      ]
    }));
  }

  registerModule() {
    this.moduleService.insertModule(this.companyId!, this.module()).subscribe({
      next: (response) => {
        console.log("Module registered successfully:", response);
        this.dialogRef.close(response);
      },
      error: (error) => {
        console.error("Error registering module:", error);
        this.dialogRef.close(null);
      }
    });
  }
}

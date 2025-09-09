import { Component, effect, inject, Input, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MATERIAL_MODULES } from '../../shared/imports/material.imports';
import { MatDialogRef } from '@angular/material/dialog';
import { ModuleService } from '../services/module.service';
import { ModuleModel } from '../models/module.model';
import { LocationModel } from '../models/location.model';
import { LocationDetailComponent } from "../components/location-detail/location-detail.component";

@Component({
  selector: 'app-module-detail',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    FormsModule,
    ReactiveFormsModule,
    LocationDetailComponent
  ],
  templateUrl: './module-detail.component.html',
  styleUrls: ['./module-detail.component.css']
})
export class ModuleDetailComponent {
  readonly dialogRef = inject(MatDialogRef<ModuleDetailComponent>);
  @Input() companyId: string | undefined;
  @Input() module = signal<ModuleModel>({
    id: '',
    name: '',
    description: '',
    locations: []
  });

  form = new FormGroup({
    name: new FormControl(''),
    description: new FormControl('')
  });

  constructor(
    private moduleService: ModuleService,
  ) {
    this.dialogRef.disableClose = true;
    this.dialogRef.updateSize("60%");

    effect(() => {
      const m = this.module();
      if (m) {
        this.form.patchValue(m, { emitEvent: false });
      }
    });

    this.form.valueChanges.subscribe(value => {
      this.module.update(m => ({
        ...m,
        name: value.name ?? '',
        description: value.description ?? ''
      }));
    });
  }

  addLocation() {
    this.module.update(m => ({
      ...m,
      locations: [
        ...(m.locations ?? []),
        {
          type: '',
          name: '',
          description: '',
          riskLimits: [{
            riskId: "",
            type: ""
          }],

        } as LocationModel
      ]
    }));
  }

  updateLocation(index: number, updated: LocationModel) {
    this.module.update(m => {
      const locations = [...(m.locations ?? [])];
      locations[index] = updated;
      return { ...m, locations };
    });
  }

  registerModule() {
    console.log("Registering module:", this.module());
    // this.moduleService.insertModule(this.companyId!, this.module()).subscribe({
    //   next: (response) => {
    //     console.log("Module registered successfully:", response);
    //     this.dialogRef.close(response);
    //   },
    //   error: (error) => {
    //     console.error("Error registering module:", error);
    //     this.dialogRef.close(null);
    //   }
    // });
  }
}

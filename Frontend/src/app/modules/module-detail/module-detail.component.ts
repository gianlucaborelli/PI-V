import { Component, computed, effect, inject, Input, signal } from '@angular/core';
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
  private readonly dialogRef = inject(MatDialogRef<ModuleDetailComponent>);
  private readonly moduleService = inject(ModuleService);

  @Input() companyId?: string;

  @Input() set module(value: ModuleModel | undefined) {
    if (value) this._module.set(value);
  }

  private _module = signal<ModuleModel>({
    id: '',
    name: '',
    description: '',
    locations: []
  });

  readonly moduleState = computed(() => this._module());

  readonly locations = computed(() => this._module().locations ?? []);

  readonly form = new FormGroup({
    name: new FormControl(''),
    description: new FormControl('')
  });

  constructor() {
    this.dialogRef.disableClose = true;
    this.dialogRef.updateSize('60%');

    this.form.valueChanges.subscribe(value => {
      this._module.update(m => ({
        ...m,
        name: value.name ?? '',
        description: value.description ?? ''
      }));
    });

    effect(() => {
      const m = this._module();
      this.form.patchValue(m, { emitEvent: false });
    });
  }

  addLocation() {
    this._module.update(m => ({
      ...m,
      locations: [
        ...(m.locations ?? []),
        {
          type: '',
          name: '',
          description: '',
          riskLimits: [{ riskId: '', type: '' }]
        } as LocationModel
      ]
    }));
  }

  updateLocation(index: number, updated: LocationModel) {
    this._module.update(m => {
      const locations = [...(m.locations ?? [])];
      locations[index] = updated;
      return { ...m, locations };
    });
  }

  saveModule() {
    const moduleValue = this._module();
    const request$ = moduleValue.id
      ? this.moduleService.updateModule(this.companyId!, moduleValue)
      : this.moduleService.insertModule(this.companyId!, moduleValue);

    request$.subscribe({
      next: response => {
        console.log('Module saved successfully:', response);
        this.dialogRef.close(response);
      },
      error: err => {
        console.error('Error saving module:', err);
        this.dialogRef.close(null);
      }
    });
  }
}

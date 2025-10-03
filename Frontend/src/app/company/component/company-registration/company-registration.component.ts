import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Component, inject, Input, model, OnInit, signal } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { CompanyModel } from '../../models/company.model';
import { CompanyService } from '../../services/company.service';
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';

@Component({
  selector: 'app-company-registration',
  standalone: true,
  imports: [
    ...MATERIAL_MODULES,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,],
  templateUrl: './company-registration.component.html',
  styleUrls: ['./company-registration.component.css']
})
export class CompanyRegistrationComponent implements OnInit {
  @Input({ required: false }) companyToEdit?: CompanyModel;

  readonly dialogRef = inject(MatDialogRef<CompanyRegistrationComponent>);
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  readonly currentTag = model('');
  company: CompanyModel = { name: '' };

  constructor(private service: CompanyService) { }

  ngOnInit(): void {
    if (this.companyToEdit) {
      this.company = { ...this.companyToEdit };
    }
  }

  registerCompany(): void {
    const request$ = this.companyToEdit
      ? this.service.updateCompany(this.company)
      : this.service.registerNewCompany(this.company);

    request$.subscribe({
      next: (response) => this.dialogRef.close(response.id),
    });
  }
}

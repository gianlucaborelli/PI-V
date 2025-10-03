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
  @Input() companyToEdit: CompanyModel | undefined;

  readonly dialogRef = inject(MatDialogRef<CompanyRegistrationComponent>);
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  readonly currentTag = model('');
  name = '';

  constructor(private service: CompanyService) { }

  ngOnInit(): void {
    if (this.companyToEdit) {
      this.name = this.companyToEdit.name;
    }
  }

  registerCompany(): void {
    if (this.companyToEdit) {
      this.companyToEdit.name = this.name;
      this.service.updateCompany(this.companyToEdit).subscribe({
        next: (response) => {
          this.dialogRef.close(response.id);
        }
      });
    }
    else {
      const company: CompanyModel = {
        name: this.name,
      };
      this.service.registerNewCompany(company).subscribe({
        next: (response) => {
          this.dialogRef.close(response.id);
        }
      });
    }
  }
}

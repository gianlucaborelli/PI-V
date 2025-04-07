import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Component, inject, model, signal } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatChipInputEvent, MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { CompanyModel } from '../../models/company.model';
import { CompanyService } from '../../services/company.service';
import { routes } from '../../../app.routes';

@Component({
  selector: 'app-company-registration',
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    FormsModule,
    MatSelectModule,
    MatChipsModule,
    MatDialogContent,
    MatDialogTitle,
    MatDialogActions,
    CommonModule,
    ReactiveFormsModule,
    MatDialogClose,
    MatFormFieldModule,
    MatInputModule],
  templateUrl: './company-registration.component.html',
  styleUrls: ['./company-registration.component.css']
})
export class CompanyRegistrationComponent {
  readonly dialogRef = inject(MatDialogRef<CompanyRegistrationComponent>);
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  readonly currentTag = model('');
  readonly tags = signal<Array<string>>([]);
  name = '';

  constructor(private service: CompanyService) { }

  removeTag(tag: string) {
    this.tags.update(tags => {
      const index = tags.indexOf(tag);
      if (index < 0) {
        return tags;
      }

      tags.splice(index, 1);
      return [...tags];
    });
  }

  addTag(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();
    if (value) {
      this.tags.update(tags => [...tags, value]);
    }
    this.currentTag.set('');
  }

  registerCompany(): void {
    const company: CompanyModel = {
      name: this.name,
      tags: this.tags(),
    };
    this.service.registerNewCompany(company).subscribe({
      next: (response) => {
        this.dialogRef.close(response.id);
      }
    });
  }
}

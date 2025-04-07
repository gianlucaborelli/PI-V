import { Component, inject, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { CompanyService } from '../../services/company.service';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { CompanyModel } from '../../models/company.model';
import { MatDialog, MatDialogConfig, MatDialogModule } from '@angular/material/dialog';
import { CompanyRegistrationComponent } from '../company-registration/company-registration.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-company-list',
  imports: [
    MatTableModule,
    MatIconModule,
    MatButtonModule,
    MatChipsModule,
    MatDialogModule
  ],
  templateUrl: './company-list.component.html',
  styleUrl: './company-list.component.css'
})
export class CompanyListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'name', 'tags', 'actions'];
  companies: CompanyModel[] = [];
  readonly dialog = inject(MatDialog);
  constructor(
    private service: CompanyService,
    private router: Router) { }

  ngOnInit(): void {
    this.service.getAllCompanies().subscribe((data: CompanyModel[]) => {
      console.log(data);
      this.companies = data;
    });
  }

  getColorForTag(tag: string): string {
    const hash = this.hashString(tag);
    return `hsl(${hash % 360}, 70%, 50%)`;
  }

  private hashString(str: string): number {
    let hash = 0;
    for (let i = 0; i < str.length; i++) {
      hash = str.charCodeAt(i) + ((hash << 5) - hash);
    }
    return hash;
  }

  editCompany(company: CompanyModel) {
    this.router.navigate([`/home/companies/${company.id}`]);
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    const dialogRef = this.dialog.open(CompanyRegistrationComponent, dialogConfig);
    return dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.router.navigate([`/home/companies/${response}`]);
      }
    });
  }
}

import { Component, inject, OnInit } from '@angular/core';
import { CompanyService } from '../../services/company.service';
import { CompanyModel } from '../../models/company.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CompanyRegistrationComponent } from '../company-registration/company-registration.component';
import { Router } from '@angular/router';
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';


@Component({
  selector: 'app-company-list',
  imports: [
    ...MATERIAL_MODULES,
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
      this.companies = data;
    });
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

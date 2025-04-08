import { Component, inject, OnInit } from '@angular/core';
import { CompanyService } from '../../services/company.service';
import { ActivatedRoute } from '@angular/router';
import { CompanyModel } from '../../models/company.model';
import { ModuleService } from '../../../modules/services/module.service';
import { ModuleModel } from '../../../modules/models/module.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CompanyRegistrationComponent } from '../company-registration/company-registration.component';
import { ModuleDetailComponent } from '../../../modules/module-detail/module-detail.component';
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';


@Component({
  selector: 'app-company-view',
  imports: [
    ...MATERIAL_MODULES,],
  templateUrl: './company-view.component.html',
  styleUrl: './company-view.component.css'
})
export class CompanyViewComponent implements OnInit {
  constructor(private companyService: CompanyService,
    private router: ActivatedRoute,
    private moduleService: ModuleService) { }

  readonly dialog = inject(MatDialog);

  displayedColumns: string[] = ['id', 'name', 'description', 'type'];

  company: CompanyModel = {
    id: ``,
    name: '',
    tags: []
  }

  modules: ModuleModel[] = [];

  ngOnInit(): void {
    this.updateDataSource();
  }

  updateDataSource() {
    const companyId = String(this.router.snapshot.paramMap.get('id'));
    this.companyService.getCompanyDetail(companyId).subscribe((data) => {
      this.company = data;
    });
    this.moduleService.getAllModules(companyId).subscribe((data) => {
      this.modules = data;
    });
  }

  translateType(type: string): string {
    const map: Record<string, string> = {
      DryBulbTemperature: 'Bulbo Seco',
      Humidity: 'Umidade',
      DarkBulbTemperature: 'Bulbo escuro'
    };
    return map[type] ?? type;
  }

  openEditCompanyDialog() {
    const dialogConfig = new MatDialogConfig();
    const dialogRef = this.dialog.open(CompanyRegistrationComponent, dialogConfig);
    dialogRef.componentInstance.companyToEdit = this.company;
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.updateDataSource();
      }
    });
  }

  openDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '80vw';
    dialogConfig.maxWidth = '90vw';
    const dialogRef = this.dialog.open(ModuleDetailComponent, dialogConfig);
    dialogRef.componentInstance.companyId = this.company.id;
    dialogRef.componentInstance.tags = this.company.tags;
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.updateDataSource();
      }
    });
  }
}

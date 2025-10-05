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
import { CommonModule } from '@angular/common';
import { SnackBarService } from '../../../shared/services/snack-bar.service';

@Component({
  selector: 'app-company-view',
  imports: [
    ...MATERIAL_MODULES, CommonModule],
  templateUrl: './company-view.component.html',
  styleUrl: './company-view.component.css'
})
export class CompanyViewComponent implements OnInit {
  constructor(private companyService: CompanyService,
    private router: ActivatedRoute,
    private snackbarService: SnackBarService,
    private moduleService: ModuleService) { }

  readonly dialog = inject(MatDialog);

  displayedColumns: string[] = ['name', 'description','risks'];

  company: CompanyModel = {
    id: ``,
    name: ''
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
      console.log(data);
    });


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
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.updateDataSource();
      }
    });
  }

  refreshModuleToken(module: any): void {
    this.moduleService.regenerateModuleToken(this.company.id!, module.id).subscribe({
      next: success => {
        if (success) {
          this.snackbarService.open("Token atualizado com sucesso.");
          this.updateDataSource();
        } else {
          this.snackbarService.open("Falha ao regenerar token.");
        }
      },
      error: err => this.snackbarService.open("Erro na requisição:", err)
    });
  }

  deleteModule(module: any): void {
    const confirmed = window.confirm(`Tem certeza que deseja excluir o módulo "${module.name}"?`);
    if (confirmed) {
      this.moduleService.deleteModule(this.company.id!, module.id).subscribe({
        next: () => {
          this.snackbarService.open("Módulo excluído com sucesso!");
          this.updateDataSource();
        },
        error: err => {
          this.snackbarService.open("Erro ao excluir módulo:", err);
        }
      });
    }
  }

  updateModule(module: any): void {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.width = '80vw';
    dialogConfig.maxWidth = '90vw';
    const dialogRef = this.dialog.open(ModuleDetailComponent, dialogConfig);
    dialogRef.componentInstance.companyId = this.company.id;
    dialogRef.componentInstance.module = module;
    dialogRef.afterClosed().subscribe((response) => {
      if (response) {
        this.updateDataSource();
      }
    });
  }
}

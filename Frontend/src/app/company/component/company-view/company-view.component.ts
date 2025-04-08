import { Component, inject, OnInit } from '@angular/core';
import { CompanyService } from '../../services/company.service';
import { ActivatedRoute } from '@angular/router';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatInputModule } from '@angular/material/input';
import { CompanyModel } from '../../models/company.model';
import { ModuleService } from '../../../modules/services/module.service';
import { ModuleModel } from '../../../modules/models/module.model';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CompanyRegistrationComponent } from '../company-registration/company-registration.component';


@Component({
  selector: 'app-company-view',
  imports: [MatExpansionModule, MatFormFieldModule, MatIconModule, MatInputModule, MatTableModule, MatChipsModule, MatButtonModule],
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

  openDialog() { }
}

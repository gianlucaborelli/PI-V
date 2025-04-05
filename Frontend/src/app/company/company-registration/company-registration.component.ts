import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
// import { CompanyService } from '../../services/company.service';


@Component({
  selector: 'app-company-registration',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './company-registration.component.html',
  styleUrls: ['./company-registration.component.css']
})
export class CompanyRegistrationComponent implements OnInit {
  // companyForm: FormGroup;

  // constructor(private fb: FormBuilder, private companyService: CompanyService) {
  //   this.companyForm = this.fb.group({
  //     name: ['', Validators.required],
  //     cnpj: ['', Validators.required],
  //     // outros campos...
  //   });
  // }

  ngOnInit(): void { }

  // onSubmit(): void {
  //   if (this.companyForm.valid) {
  //     this.companyService.registerCompany(this.companyForm.value).subscribe({
  //       next: (result) => {
  //         // Lógica de sucesso (por exemplo, redirecionar ou exibir mensagem)
  //         console.log('Empresa cadastrada:', result);
  //       },
  //       error: (error) => console.error('Erro ao cadastrar empresa:', error)
  //     });
  //   }
  // }
}

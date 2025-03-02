import { Component } from '@angular/core';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';

import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';
import { SnackBarService } from '../../../components/snack-bar/service/snack-bar.service';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-sign-in',
  imports: [
    MatCardModule,
    MatFormFieldModule,
    MatTooltipModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    NgIf
  ],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent {
  faCoffee = faCoffee;

  showLogin = true;
  showForgotPassword = false;
  showRegister = false;

  constructor(
    public authService: AuthService,
    private router: Router,
    private snackbarService: SnackBarService,
  ) { }

  toggleForm(formType: string) {
    this.showLogin = formType === 'login';
    this.showForgotPassword = formType === 'forgotPassword';
    this.showRegister = formType === 'register';
  }

  async Login(userName: string, pass: string) {
    this.authService.login({ password: pass, email: userName }).subscribe({
      next: (resp) => {
        this.authService.registryOnLocalStorage(resp);

        this.router.navigate(['dashboard']);
      },
      error: (err) => {
        console.error(err);

        let errorMessage = "Ocorreu um erro desconhecido.";

        if (err.error?.errors) {
          errorMessage = Object.values(err.error.errors)
            .flat()
            .join("<br>");
        }

        this.snackbarService.open(errorMessage);
      }
    });
  }

  async Register(userName: string, pass: string, email: string, userconfirmPassword: string) {
    this.authService.register({ name: userName, password: pass, email: email, confirmPassword: userconfirmPassword }).subscribe({
      next: (res) => {
        console.log(res);
        this.snackbarService.open("Registro criado com sucesso. Faça seu Login.");
        this.toggleForm('login');
      },
      error: (err) => {
        console.error(err);

        let errorMessage = "Ocorreu um erro desconhecido.";

        if (err.error?.errors) {
          errorMessage = Object.values(err.error.errors)
            .flat()
            .join("\n");
        }

        this.snackbarService.open(errorMessage);
      }
    });
  }

  NotImplemented() {
    this.snackbarService.open("Não implementado.");
  }
}

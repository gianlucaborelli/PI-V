import { Component } from '@angular/core';
import { faCoffee } from '@fortawesome/free-solid-svg-icons';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';

import { NgIf } from '@angular/common';
import { MATERIAL_MODULES } from '../../../shared/imports/material.imports';
import { SnackBarService } from '../../../shared/services/snack-bar.service';

@Component({
  selector: 'app-sign-in',
  imports: [
    ...MATERIAL_MODULES,
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

        this.router.navigate(['home']);
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

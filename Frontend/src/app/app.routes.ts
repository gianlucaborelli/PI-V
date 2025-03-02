import { Routes } from '@angular/router';
import { SignInComponent } from './authentication/view/sign-in/sign-in.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AuthGuard } from './authentication/guard/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: SignInComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    children: []
  }
];

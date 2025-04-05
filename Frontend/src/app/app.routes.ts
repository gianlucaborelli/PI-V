import { Routes } from '@angular/router';
import { SignInComponent } from './authentication/view/sign-in/sign-in.component';
import { CompanyRegistrationComponent } from './company/company-registration/company-registration.component';
import { SensorRegistrationComponent } from './sensores/sensor-registration/sensor-registration.component';
import { AuthGuard } from './authentication/guard/auth.guard';

import { HomeComponent } from './shared/home/home.component';
import { DashboardComponent } from './dashboard/component/dashboard.component';

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
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuard],
      }
    ]
  },
];

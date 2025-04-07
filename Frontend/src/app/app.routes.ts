import { Routes } from '@angular/router';
import { SignInComponent } from './authentication/view/sign-in/sign-in.component';
import { AuthGuard } from './authentication/guard/auth.guard';

import { HomeComponent } from './shared/home/home.component';
import { DashboardComponent } from './dashboard/component/dashboard.component';
import { CompanyListComponent } from './company/component/company-list/company-list.component';
import { CompanyViewComponent } from './company/component/company-view/company-view.component';

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
      },
      {
        path: 'companies',
        component: CompanyListComponent,
        canActivate: [AuthGuard],
      },
      {
        path: 'companies/:id',
        component: CompanyViewComponent,
        canActivate: [AuthGuard]
      }
    ]
  },
];

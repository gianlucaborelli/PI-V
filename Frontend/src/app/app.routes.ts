import { Routes } from '@angular/router';
import { SignInComponent } from './authentication/view/sign-in/sign-in.component';
import { DashboardComponent } from './dashboards/dashboard.component';
import { CompanyRegistrationComponent } from './company/company-registration/company-registration.component';
import { ModuleRegistrationComponent } from './modules/module-registration/module-registration.component';
import { SensorRegistrationComponent } from './sensores/sensor-registration/sensor-registration.component';
import { AuthGuard } from './authentication/guard/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: SignInComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'company/register', component: CompanyRegistrationComponent, canActivate: [AuthGuard] },
  { path: 'module/register', component: ModuleRegistrationComponent, canActivate: [AuthGuard] },
  { path: 'sensor/register', component: SensorRegistrationComponent, canActivate: [AuthGuard] }
];

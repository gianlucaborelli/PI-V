import { Component, ViewChild } from '@angular/core';
import { AuthService } from '../../authentication/service/auth.service';
import { MatSidenav } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';
import { delay, filter } from 'rxjs';
import { NavigationEnd, Router } from '@angular/router';
import { UserStoreService } from '../../authentication/service/user-store.service';
import { RouterOutlet } from '@angular/router';
import { RouterModule } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { NgIf } from '@angular/common';
import { MATERIAL_MODULES } from '../imports/material.imports';

@UntilDestroy()
@Component({
  selector: 'app-home',
  imports: [
    ...MATERIAL_MODULES,
    RouterOutlet,
    RouterModule,
    NgIf
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  constructor(
    public authService: AuthService,
    private observer: BreakpointObserver,
    private router: Router,
    public user: UserStoreService) { }

  ngAfterViewInit() {
    this.observer
      .observe(['(max-width: 800px)'])
      .pipe(delay(1), untilDestroyed(this))
      .subscribe((res) => {
        if (res.matches) {
          this.sidenav.mode = 'over';
          this.sidenav.close();
        } else {
          this.sidenav.mode = 'side';
          this.sidenav.open();
        }
      });

    this.router.events
      .pipe(
        untilDestroyed(this),
        filter((e) => e instanceof NavigationEnd)
      )
      .subscribe(() => {
        if (this.sidenav.mode === 'over') {
          this.sidenav.close();
        }
      });
  }
}


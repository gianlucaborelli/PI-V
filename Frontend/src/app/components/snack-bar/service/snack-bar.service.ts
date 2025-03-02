import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SnackBarComponent } from '../component/snack-bar/snack-bar.component';

@Injectable({
  providedIn: 'root'
})
export class SnackBarService {

  constructor(private snackBar: MatSnackBar) { }

  open(message: string, action: string = "Ok", duration: number = 3) {
    return this.snackBar.openFromComponent(SnackBarComponent, {
      data: { message, action },
      duration: duration * 1000
    });
  }
}

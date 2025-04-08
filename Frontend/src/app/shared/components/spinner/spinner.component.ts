import { Component, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SpinnerService } from '../../services/spinner.service';

@Component({
  selector: 'app-spinner',
  imports: [CommonModule],
  templateUrl: './spinner.component.html',
  styleUrl: './spinner.component.css',
  encapsulation: ViewEncapsulation.ShadowDom
})
export class SpinnerComponent {
  constructor(public loader: SpinnerService) { }
}

import { Component, Input } from '@angular/core';
import {NgIf, NgStyle} from "@angular/common";

@Component({
  selector: 'app-progress-bar',
  standalone: true,
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.css'],
  imports: [
    NgStyle,
    NgIf
  ]
})
export class ProgressBarComponent {
  @Input() label: string = '';
  @Input() progress: number = 0; // значение от 0 до 100
  @Input() color: string = '#4caf50';
}

import { Component, Input, Output, EventEmitter } from '@angular/core';
import { WaterService } from '../../../services/water-service';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-add-water',
  standalone: true,
  templateUrl: './add-water.component.html',
  styleUrls: ['./add-water.component.css'],
  imports: [
    FormsModule
  ]
})
export class AddWaterComponent {
  @Input() userId: string = '';
  @Output() waterAdded = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  glassesDrunk: number = 0;

  constructor(private waterService: WaterService) {}

  addWater(): void {
    if (this.glassesDrunk > 0) {
      this.waterService.addWaterLog(this.userId, this.glassesDrunk).subscribe({
        next: () => {
          this.waterAdded.emit();
        },
        error: (err) => console.error('Ошибка при добавлении воды', err),
      });
    }
  }
}

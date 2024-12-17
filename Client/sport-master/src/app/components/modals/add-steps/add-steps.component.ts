import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StepService } from '../../../services/step-service';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-add-steps',
  standalone: true,
  templateUrl: './add-steps.component.html',
  styleUrls: ['./add-steps.component.css'],
  imports: [
    FormsModule
  ]
})
export class AddStepsComponent {
  @Input() userId: string = '';
  @Output() stepsAdded = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  stepsCount: number = 0;

  constructor(private stepService: StepService) {}

  addSteps(): void {
    if (this.stepsCount > 0) {
      this.stepService.addStepLog(this.userId, this.stepsCount).subscribe({
        next: () => {
          this.stepsAdded.emit();
        },
        error: (err) => console.error('Ошибка при добавлении шагов', err),
      });
    }
  }
}

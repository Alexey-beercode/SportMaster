import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ExerciseService } from '../../../services/exercise-service';
import { EnumService } from '../../../services/enum-service';
import { ExerciseLogRequestDTO } from '../../../models/dtos-request';
import {FormsModule} from "@angular/forms";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-add-exercise',
  standalone: true,
  templateUrl: './add-exercise.component.html',
  styleUrls: ['./add-exercise.component.css'],
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ]
})
export class AddExerciseComponent implements OnInit {
  @Input() userId: string = '';
  @Output() exerciseAdded = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  exerciseTypes = [
    { label: 'Бег', caloriesPerHour: 750 },
    { label: 'Плавание', caloriesPerHour: 600 },
    { label: 'Ходьба', caloriesPerHour: 300 },
    { label: 'Прыжки на скакалке', caloriesPerHour: 850 },
    { label: 'Езда на велосипеде', caloriesPerHour: 500 },
    { label: 'Аэробика', caloriesPerHour: 500 },
    { label: 'Тренажерный зал', caloriesPerHour: 700 },
  ];

  selectedExercise: string | null = null;
  duration: number | null = null; // Продолжительность в минутах
  caloriesBurned: number | null = null; // Автоматически рассчитывается

  constructor(private exerciseService: ExerciseService) {}

  ngOnInit(): void {}

  calculateCalories(): void {
    if (this.selectedExercise && this.duration) {
      const exercise = this.exerciseTypes.find(
        (type) => type.label === this.selectedExercise
      );
      if (exercise) {
        this.caloriesBurned = Math.round(
          (exercise.caloriesPerHour / 60) * this.duration
        );
      }
    }
  }

  addExercise(): void {
    if (!this.selectedExercise || !this.duration || !this.caloriesBurned) return;

    const exerciseLog: ExerciseLogRequestDTO = {
      userId: this.userId,
      date: new Date().toISOString(),
      exerciseType: this.selectedExercise,
      duration: this.duration,
      caloriesBurned: this.caloriesBurned,
    };

    this.exerciseService.addExerciseLog(exerciseLog).subscribe({
      next: () => {
        this.exerciseAdded.emit();
      },
      error: (err) => console.error('Ошибка при добавлении упражнения', err),
    });
  }
}

import { Component, Input, OnInit } from '@angular/core';
import { CalorieService } from '../../../services/calorie-service';
import { GoalService } from '../../../services/goal-service';
import { UserCaloriesDTO, ProgressDto } from '../../../models/dtos-response';
import { ProgressBarComponent } from '../../shared/progress-bar/progress-bar.component';
import {NgStyle} from "@angular/common";

@Component({
  selector: 'app-calorie-progress',
  standalone: true,
  imports: [ProgressBarComponent, NgStyle],
  templateUrl: 'calorie-progress.component.html',
  styleUrls: ['./calorie-progress.component.css'],
})
export class CalorieProgressComponent implements OnInit {
  @Input() userId: string = '';

  calorieData: UserCaloriesDTO | null = null; // Данные о норме калорий
  progressData: ProgressDto | null = null; // Данные о потребленных и потраченных калориях

  consumedPercentage: number = 0;
  burnedPercentage: number = 0;

  constructor(
    private calorieService: CalorieService,
    private goalService: GoalService
  ) {}

  ngOnInit(): void {
    this.loadCalorieData();
    this.loadProgressData();
  }

  loadCalorieData(): void {
    this.calorieService.calculateDailyCalories(this.userId).subscribe({
      next: (data) => {
        this.calorieData = data;
        this.updateProgress();
      },
      error: (err) => console.error('Ошибка при получении нормы калорий', err),
    });
  }

  loadProgressData(): void {
    this.goalService.getProgress(this.userId).subscribe({
      next: (data) => {
        this.progressData = data;
        this.updateProgress();
      },
      error: (err) => console.error('Ошибка при получении прогресса калорий', err),
    });
  }

  updateProgress(): void {
    console.log('calorieData:', this.calorieData);
    console.log('progressData:', this.progressData);

    if (
      this.calorieData?.caloriesNorm &&
      this.progressData?.caloriesConsumed != null &&
      this.progressData?.caloriesBurned != null
    ) {
      const { caloriesNorm } = this.calorieData;
      const { caloriesConsumed, caloriesBurned } = this.progressData;

      this.consumedPercentage = Math.round((caloriesConsumed / caloriesNorm) * 100);
      this.burnedPercentage = Math.round((caloriesBurned / caloriesNorm) * 100);
    } else {
      console.error('Некорректные данные для прогресса калорий');
    }
  }

}

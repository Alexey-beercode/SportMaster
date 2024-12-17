import { Component, OnInit } from '@angular/core';
import { ExerciseService } from '../../services/exercise-service';
import { FoodService } from '../../services/food-service';
import { ExerciseLogDto, FoodLogDto } from '../../models/dtos-response';
import {HeaderComponent} from "../../components/shared/header/header.component";
import {ActivityChartComponent} from "../../components/statistics/activity-chart/activity-chart.component";
import {CaloriesChartComponent} from "../../components/statistics/calories-chart/calories-chart.component";
import {NgIf} from "@angular/common";
import {TokenService} from "../../services/token.service";

@Component({
  selector: 'app-statistics-page',
  templateUrl: './statistics-page.component.html',
  styleUrls: ['./statistics-page.component.css'],
  imports: [
    HeaderComponent,
    ActivityChartComponent,
    CaloriesChartComponent,
    NgIf
  ],
  standalone: true
})
export class StatisticsPageComponent implements OnInit {
  activeTab: 'activity' | 'calories' = 'activity'; // Переключение между вкладками
  selectedPeriod: 'week' | 'month' | 'year' = 'week'; // Временной промежуток
  userId: string = ''; // Пример ID пользователя, заменить на реальный

  exerciseLogs: ExerciseLogDto[] = [];
  foodLogs: FoodLogDto[] = [];

  constructor(
    private exerciseService: ExerciseService,
    private foodService: FoodService,
    private tokenService:TokenService
  ) {}

  ngOnInit(): void {
    this.userId=this.tokenService.getUserId()!;
    this.loadData();
  }

  loadData(): void {
    const { startDate, endDate } = this.getDateRange();
    this.exerciseService.getExerciseLogs(this.userId, startDate, endDate).subscribe((data) => {
      this.exerciseLogs = data;
    });

    this.foodService.getFoodLogs(this.userId, startDate, endDate).subscribe((data) => {
      this.foodLogs = data;
    });
  }

  // Получаем диапазон дат для выбранного периода
  getDateRange(): { startDate: string; endDate: string } {
    const today = new Date();
    let startDate = new Date();

    switch (this.selectedPeriod) {
      case 'week':
        startDate.setDate(today.getDate() - 7);
        break;
      case 'month':
        startDate.setMonth(today.getMonth() - 1);
        break;
      case 'year':
        startDate.setFullYear(today.getFullYear() - 1);
        break;
    }

    return {
      startDate: startDate.toISOString(),
      endDate: today.toISOString(),
    };
  }

  setTab(tab: 'activity' | 'calories') {
    this.activeTab = tab;
    this.loadData();
  }

  setPeriod(period: 'week' | 'month' | 'year') {
    this.selectedPeriod = period;
    this.loadData();
  }
}

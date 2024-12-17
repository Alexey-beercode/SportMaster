import { Component, ViewChild } from '@angular/core';
import { WeightSummaryComponent } from '../../components/home/weight-summary/weight-summary.component';
import { CalorieProgressComponent } from '../../components/home/calorie-progress/calorie-progress.component';
import { StepsProgressComponent } from '../../components/home/steps-progress/steps-progress.component';
import { WaterProgressComponent } from '../../components/home/water-progress/water-progress.component';
import { AddMealComponent } from '../../components/modals/add-meal/add-meal.component';
import { AddExerciseComponent } from '../../components/modals/add-exercise/add-exercise.component';
import { HeaderComponent } from "../../components/shared/header/header.component";
import { NgIf } from "@angular/common";
import { TokenService } from "../../services/token.service";
import {ActionHistoryModalComponent} from "../../components/profile/action-history/action-history.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    WeightSummaryComponent,
    CalorieProgressComponent,
    StepsProgressComponent,
    WaterProgressComponent,
    AddMealComponent,
    AddExerciseComponent,
    HeaderComponent,
    NgIf,
    ActionHistoryModalComponent,
  ],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css'],
})
export class HomePageComponent {
  userId: string = '';
  showAddMealModal: boolean = false;
  showAddExerciseModal: boolean = false;
  showActionHistoryModal: boolean = false;

  // Получаем ссылки на дочерние компоненты
  @ViewChild(WeightSummaryComponent) weightSummaryComponent!: WeightSummaryComponent;
  @ViewChild(CalorieProgressComponent) calorieProgressComponent!: CalorieProgressComponent;
  @ViewChild(StepsProgressComponent) stepsProgressComponent!: StepsProgressComponent;
  @ViewChild(WaterProgressComponent) waterProgressComponent!: WaterProgressComponent;

  constructor(private tokenService: TokenService) {
    this.userId = this.tokenService.getUserId()!;
  }

  openAddMealModal(): void {
    this.showAddMealModal = true;
  }

  closeAddMealModal(): void {
    this.showAddMealModal = false;
    this.refreshData(); // Обновляем данные
  }

  openAddExerciseModal(): void {
    this.showAddExerciseModal = true;
  }

  closeAddExerciseModal(): void {
    this.showAddExerciseModal = false;
    this.refreshData(); // Обновляем данные
  }

  // Метод для обновления всех дочерних компонентов
  refreshData(): void {
    if (this.weightSummaryComponent) this.weightSummaryComponent.ngOnInit();
    if (this.calorieProgressComponent) {
      this.calorieProgressComponent.loadCalorieData();
      this.calorieProgressComponent.loadProgressData();
    }
    if (this.stepsProgressComponent) this.stepsProgressComponent.loadSteps();
    if (this.waterProgressComponent) this.waterProgressComponent.updateProgress();
  }


}

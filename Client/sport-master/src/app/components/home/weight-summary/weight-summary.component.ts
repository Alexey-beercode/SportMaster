import {Component, Input, OnInit} from '@angular/core';
import {GoalService} from '../../../services/goal-service';
import {UserService} from '../../../services/user-service';
import {GoalDto, GoalWithProgressDTO, UserDto} from '../../../models/dtos-response';
import {CommonModule} from "@angular/common";
import {GoalType} from "../../../models/enums";

@Component({
  selector: 'app-weight-summary',
  standalone: true,
  templateUrl: './weight-summary.component.html',
  styleUrls: ['./weight-summary.component.css'],
  imports: [CommonModule]
})
export class WeightSummaryComponent implements OnInit {
  @Input() userId: string = '';

  user: UserDto | null = null;
  weightGoal: GoalDto | null = null;

  constructor(
    private userService: UserService,
    private goalService: GoalService
  ) {}

  ngOnInit(): void {
    this.loadUserData();
    this.loadWeightGoal();
  }

  loadUserData(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (err) => console.error('Ошибка при загрузке данных пользователя', err),
    });
  }

  loadWeightGoal(): void {
    this.goalService.getUserGoalsWithoutProgress(this.userId).subscribe({
      next: (goals) => {
        console.log('Полученные цели:', goals);
        this.weightGoal = goals.find(
          (goal) =>
            (goal.goalType === GoalType.WeightLoss || goal.goalType === GoalType.MuscleGain) &&
            goal.targetWeight != null // Проверяем, что targetWeight не равен null или undefined
        ) || null;

      },
      error: (err) => console.error('Ошибка при загрузке целей', err),
    });
  }


  getWeightStatus(): string {
    if (!this.user || !this.weightGoal){
      return '';
    }

    const currentWeight = this.user.weight;
    const targetWeight = this.weightGoal.targetWeight;
    if (targetWeight === undefined || targetWeight === null) return 'Цель по весу не задана.';

    if (currentWeight === targetWeight) return 'Цель достигнута!';
    return currentWeight > targetWeight
      ? 'Необходимо сбросить вес.'
      : 'Необходимо набрать вес.';
  }
}

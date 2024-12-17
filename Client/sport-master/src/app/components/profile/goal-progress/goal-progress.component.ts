import { take } from 'rxjs';
import {ProgressBarComponent} from "../../shared/progress-bar/progress-bar.component";
import {NgForOf, NgIf} from "@angular/common";
import {Component, OnInit} from "@angular/core";
import {GoalDto, ProgressDto} from "../../../models/dtos-response";
import {GoalService} from "../../../services/goal-service";
import {TokenService} from "../../../services/token.service";
import {GoalType} from "../../../models/enums";

@Component({
  selector: 'app-goal-progress',
  standalone: true,
  imports: [ProgressBarComponent, NgForOf, NgIf],
  templateUrl: './goal-progress.component.html',
  styleUrls: ['./goal-progress.component.css'],
})
export class GoalProgressComponent implements OnInit {
  userId: string | null = '';
  goals: GoalDto[] = [];
  todayProgress: ProgressDto | null = null;
  notifications: { id: string; message: string }[] = [];

  constructor(
    private goalService: GoalService,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    console.log('GoalProgressComponent initialized');
    this.userId = this.tokenService.getUserId();
    if (this.userId) {
      this.loadGoals();
      this.loadTodayProgress();
    }
  }

  loadGoals(): void {
    this.goalService.getUserGoalsWithoutProgress(this.userId!).pipe(take(1)).subscribe({
      next: (goals) => {
        console.log('API response:', goals);
        this.goals = this.removeDuplicateGoals(goals);
        this.checkCompletedGoals();
      },
      error: (err) => console.error('Ошибка при получении целей', err),
    });
  }

  loadTodayProgress(): void {
    this.goalService.getProgress(this.userId!).pipe(take(1)).subscribe({
      next: (progress) => {
        this.todayProgress = progress;
      },
      error: (err) => console.error('Ошибка при получении прогресса', err),
    });
  }

  removeDuplicateGoals(goals: GoalDto[]): GoalDto[] {
    const seen = new Set<string>();
    return goals.filter((goal) => {
      if (seen.has(goal.id)) return false;
      seen.add(goal.id);
      return true;
    });
  }

  trackByGoalId(index: number, goal: GoalDto): string {
    return goal.id;
  }

  calculateWeightProgress(goal: GoalDto): number {
    if (!goal.targetWeight || !this.todayProgress?.weight) return 0;
    const weightDifference = Math.abs(goal.targetWeight - this.todayProgress.weight);
    return Math.max(0, 100 - (weightDifference / goal.targetWeight) * 100);
  }

  calculateCaloriesConsumedProgress(goal: GoalDto): number {
    if (!goal.dailyCalorieIntake || !this.todayProgress?.caloriesConsumed) return 0;
    return Math.min(100, (this.todayProgress.caloriesConsumed / goal.dailyCalorieIntake) * 100);
  }

  calculateCaloriesBurnedProgress(goal: GoalDto): number {
    if (!goal.dailyCalorieBurn || !this.todayProgress?.caloriesBurned) return 0;
    return Math.min(100, (this.todayProgress.caloriesBurned / goal.dailyCalorieBurn) * 100);
  }

  getProgressColor(progress: number): string {
    return progress >= 100 ? '#ff9800' : '#4caf50';
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString('ru-RU');
  }

  getGoalTypeLabel(goalType: GoalType): string {
    const goalTypeMap: { [key: string]: string } = {
      WeightLoss: 'Похудение',
      MuscleGain: 'Набор мышц',
      Maintenance: 'Поддержание веса',
    };
    return goalTypeMap[goalType] || 'Неизвестная цель';
  }

  checkCompletedGoals(): void {
    if (!this.todayProgress) return;

    this.goals.forEach((goal) => {
      if (
        (this.isWeightGoalCompleted(goal) ||
          this.isCaloriesConsumedGoalCompleted(goal) ||
          this.isCaloriesBurnedGoalCompleted(goal)) &&
        !this.isNotificationShown(goal.id)
      ) {
        console.log("найдена выполненная цель")
        const message = `🎉 Поздравляем! Цель "${this.getGoalTypeLabel(goal.goalType)}" выполнена!`;
        this.notifications.push({ id: goal.id, message });
        this.markNotificationAsShown(goal.id);
      }
    });
  }

  isWeightGoalCompleted(goal: GoalDto): boolean {
    return !!(
      goal.targetWeight &&
      this.todayProgress?.weight &&
      this.todayProgress.weight === goal.targetWeight
    );
  }

  isCaloriesConsumedGoalCompleted(goal: GoalDto): boolean {
    return !!(
      goal.dailyCalorieIntake &&
      this.todayProgress?.caloriesConsumed &&
      this.todayProgress.caloriesConsumed >= goal.dailyCalorieIntake
    );
  }

  isCaloriesBurnedGoalCompleted(goal: GoalDto): boolean {
    return !!(
      goal.dailyCalorieBurn &&
      this.todayProgress?.caloriesBurned &&
      this.todayProgress.caloriesBurned >= goal.dailyCalorieBurn
    );
  }

  isNotificationShown(goalId: string): boolean {
    const shownGoals = JSON.parse(localStorage.getItem('completedGoals') || '[]');
    return shownGoals.includes(goalId);
  }

  markNotificationAsShown(goalId: string): void {
    const shownGoals = JSON.parse(localStorage.getItem('completedGoals') || '[]');
    shownGoals.push(goalId);
    localStorage.setItem('completedGoals', JSON.stringify(shownGoals));
  }

}

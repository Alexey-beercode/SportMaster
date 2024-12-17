import { Component, Input, OnInit } from '@angular/core';
import { StepService } from '../../../services/step-service';
import { UserService } from '../../../services/user-service';
import { UserDto, StepLogDTO } from '../../../models/dtos-response';
import { ProgressBarComponent } from '../../shared/progress-bar/progress-bar.component';
import { AddStepsComponent } from '../../modals/add-steps/add-steps.component';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-steps-progress',
  standalone: true,
  imports: [ProgressBarComponent, AddStepsComponent, NgIf],
  templateUrl: './steps-progress.component.html',
  styleUrls: ['./steps-progress.component.css'],
})
export class StepsProgressComponent implements OnInit {
  @Input() userId: string = '';
  stepGoal: number = 0; // Цель пользователя по шагам
  totalSteps: number = 0; // Суммарное количество шагов за день
  stepsPercentage: number = 0;

  user: UserDto | null = null;
  showAddStepsModal: boolean = false;

  constructor(private stepService: StepService, private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (userData: UserDto) => {
        this.user = userData;
        this.stepGoal = userData.dailyStepGoal || 10000; // Устанавливаем норму шагов
        this.loadSteps();
      },
      error: (err) => console.error('Ошибка при получении пользователя', err),
    });
  }

  loadSteps(): void {
    const today = new Date().toISOString().split('T')[0];
    const tomorrow = new Date(new Date().setDate(new Date().getDate() + 1))
      .toISOString()
      .split('T')[0];

    this.stepService.getStepLogs(this.userId, today, tomorrow).subscribe({
      next: (data: StepLogDTO[]) => {
        this.totalSteps = data.reduce((sum, log) => sum + log.stepsCount, 0);
        this.updateProgress();
      },
      error: (err) => console.error('Ошибка при получении шагов', err),
    });
  }

  updateProgress(): void {
    this.stepsPercentage = Math.min(
      Math.round((this.totalSteps / this.stepGoal) * 100),
      100
    );
  }

  openAddStepsModal(): void {
    this.showAddStepsModal = true;
  }

  closeAddStepsModal(): void {
    this.showAddStepsModal = false;
  }

  handleStepsAdded(): void {
    this.closeAddStepsModal();
    this.loadSteps();
  }
}

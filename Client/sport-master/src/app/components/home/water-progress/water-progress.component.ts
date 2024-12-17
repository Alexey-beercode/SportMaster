import { Component, Input, OnInit } from '@angular/core';
import { WaterService } from '../../../services/water-service';
import { UserService } from '../../../services/user-service';
import { UserDto, WaterLogDTO } from '../../../models/dtos-response';
import { ProgressBarComponent } from '../../shared/progress-bar/progress-bar.component';
import { AddWaterComponent } from '../../modals/add-water/add-water.component';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-water-progress',
  standalone: true,
  imports: [ProgressBarComponent, AddWaterComponent, NgIf],
  templateUrl: './water-progress.component.html',
  styleUrls: ['./water-progress.component.css'],
})
export class WaterProgressComponent implements OnInit {
  @Input() userId: string = '';
  dailyWaterGoal: number = 0; // Цель пользователя по воде
  totalGlasses: number = 0; // Суммарное количество стаканов за день
  waterPercentage: number = 0;

  user: UserDto | null = null; // Данные пользователя
  showAddWaterModal: boolean = false;

  constructor(private waterService: WaterService, private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserData();
  }

  loadUserData(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (userData: UserDto) => {
        this.user = userData;
        this.dailyWaterGoal = userData.dailyWaterGoal || 8; // Устанавливаем норму воды
        this.loadWaterLogs();
      },
      error: (err) => console.error('Ошибка при получении пользователя', err),
    });
  }

  loadWaterLogs(): void {
    const today = new Date().toISOString().split('T')[0]; // Текущая дата
    const tomorrow = new Date(new Date().setDate(new Date().getDate() + 1))
      .toISOString()
      .split('T')[0];

    this.waterService.getWaterLogs(this.userId, today, tomorrow).subscribe({
      next: (data: WaterLogDTO[]) => {
        this.totalGlasses = data.reduce((sum, log) => sum + log.glassesDrunk, 0);
        this.updateProgress();
      },
      error: (err) => console.error('Ошибка при получении данных о воде', err),
    });
  }

  updateProgress(): void {
    this.waterPercentage = Math.min(
      Math.round((this.totalGlasses / this.dailyWaterGoal) * 100),
      100
    );
  }

  openAddWaterModal(): void {
    this.showAddWaterModal = true;
  }

  closeAddWaterModal(): void {
    this.showAddWaterModal = false;
  }

  handleWaterAdded(): void {
    this.closeAddWaterModal();
    this.loadWaterLogs();
  }
}

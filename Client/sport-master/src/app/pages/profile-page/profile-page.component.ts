import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user-service';
import { GoalService } from '../../services/goal-service';
import { TokenService } from '../../services/token.service';
import {UserDto, GoalDto, GoalWithProgressDTO} from '../../models/dtos-response';
import { GoalProgressComponent } from '../../components/profile/goal-progress/goal-progress.component';
import { RecommendationComponent } from '../../components/profile/recommendation/recommendation.component';
import { HeaderComponent } from '../../components/shared/header/header.component';
import { NgIf, NgFor } from '@angular/common';
import {EditUserModalComponent} from "../../components/profile/edit-profile/edit-profile.component";
import {AddGoalComponent} from "../../components/profile/add-goal/add-goal.component";
import {ActionHistoryModalComponent} from "../../components/profile/action-history/action-history.component";

@Component({
  selector: 'app-profile-page',
  standalone: true,
  imports: [
    HeaderComponent,
    EditUserModalComponent,
    GoalProgressComponent,
    RecommendationComponent,
    NgIf,
    NgFor,
    AddGoalComponent,
    ActionHistoryModalComponent,
  ],
  templateUrl: './profile-page.component.html',
  styleUrls: ['./profile-page.component.css'],
})
export class ProfilePageComponent implements OnInit {
  userId: string = '';
  userData: UserDto | null = null;
  goals: GoalWithProgressDTO[] = [];
  isEditModalOpen: boolean = false;
  showAddGoalModal: boolean = false;
  showActionHistoryModal: boolean = false;

  constructor(
    private userService: UserService,
    private goalService: GoalService,
    private tokenService: TokenService
  ) {
    this.userId = this.tokenService.getUserId()!;
  }

  ngOnInit(): void {
    this.loadUserData();
    this.loadUserGoals();
  }

  loadUserData(): void {
    this.userService.getUserById(this.userId).subscribe({
      next: (data: UserDto) => {
        this.userData = data;
      },
      error: (err) => console.error('Ошибка при загрузке данных пользователя', err),
    });
  }

  loadUserGoals(): void {
    this.goalService.getUserGoals(this.userId).subscribe({
      next: (data: GoalWithProgressDTO[]) => {
        this.goals = data;
      },
      error: (err) => console.error('Ошибка при загрузке целей пользователя', err),
    });
  }

  openAddGoalModal(): void {
    this.showAddGoalModal = true;
  }

  closeAddGoalModal(): void {
    this.showAddGoalModal = false;
  }

  onGoalAdded(): void {
    this.showAddGoalModal = false;
    this.loadUserGoals(); // Обновляем список целей после добавления
  }

  openEditModal(): void {
    this.isEditModalOpen = true;
  }

  closeEditModal(): void {
    this.isEditModalOpen = false;
    this.loadUserData(); // Обновляем данные пользователя после редактирования
  }
  openActionHistoryModal(): void {
    this.showActionHistoryModal = true;
  }

  closeActionHistoryModal(): void {
    this.showActionHistoryModal = false;
  }
}

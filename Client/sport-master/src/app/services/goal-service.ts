import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import {GoalDto, CustomGoalDto, ProgressDto, GoalWithProgressDTO} from '../models/dtos-response';
import { CreateGoalRequestDTO, CreateCustomGoalRequestDTO } from '../models/dtos-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GoalService {
  private readonly baseUrl = `${environment.baseUrl}/api/goals`;

  constructor(private http: HttpClient) {}

  getUserGoals(userId: string): Observable<GoalWithProgressDTO[]> {
    return this.http.get<GoalWithProgressDTO[]>(`${this.baseUrl}/user/${userId}`);
  }

  addGoal(goalRequest: CreateGoalRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, goalRequest);
  }

  getProgress(userId: string): Observable<ProgressDto> {
    return this.http.get<ProgressDto>(`${this.baseUrl}/user/${userId}/progress`);
  }

  getCustomGoals(userId: string): Observable<CustomGoalDto[]> {
    return this.http.get<CustomGoalDto[]>(`${this.baseUrl}/user/${userId}/custom-goals`);
  }

  addCustomGoal(customGoalRequest: CreateCustomGoalRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/custom-goal`, customGoalRequest);
  }
  getUserGoalsWithoutProgress(userId: string): Observable<GoalDto[]> {
    return this.http.get<GoalDto[]>(`${this.baseUrl}/user/${userId}/without-progress`);
  }

}

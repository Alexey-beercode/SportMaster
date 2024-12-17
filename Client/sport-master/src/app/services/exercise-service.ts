import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { ExerciseLogDto } from '../models/dtos-response';
import { ExerciseLogRequestDTO } from '../models/dtos-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ExerciseService {
  private readonly baseUrl = `${environment.baseUrl}/api/exercises`;

  constructor(private http: HttpClient) {}

  getExerciseLogs(userId: string, startDate?: string, endDate?: string): Observable<ExerciseLogDto[]> {
    const params: any = {};
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;

    return this.http.get<ExerciseLogDto[]>(`${this.baseUrl}/user/${userId}`, { params });
  }

  addExerciseLog(exerciseLogRequest: ExerciseLogRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, exerciseLogRequest);
  }
}

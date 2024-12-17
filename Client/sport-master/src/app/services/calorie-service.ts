import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { UserCaloriesDTO } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CalorieService {
  private readonly baseUrl = `${environment.baseUrl}/api/calories`;

  constructor(private http: HttpClient) {}

  calculateDailyCalories(userId: string): Observable<UserCaloriesDTO> {
    return this.http.get<UserCaloriesDTO>(`${this.baseUrl}/user/${userId}`);
  }
}

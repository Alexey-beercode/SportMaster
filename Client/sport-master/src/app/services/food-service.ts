import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { FoodLogDto } from '../models/dtos-response';
import { FoodLogRequestDTO } from '../models/dtos-request';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FoodService {
  private readonly baseUrl = `${environment.baseUrl}/api/food`;

  constructor(private http: HttpClient) {}

  getFoodLogs(userId: string, startDate?: string, endDate?: string): Observable<FoodLogDto[]> {
    const params: any = {};
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;

    return this.http.get<FoodLogDto[]>(`${this.baseUrl}/user/${userId}`, { params });
  }

  addFoodLog(foodLogRequest: FoodLogRequestDTO): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}`, foodLogRequest);
  }
}

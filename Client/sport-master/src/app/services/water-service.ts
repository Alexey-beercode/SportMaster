import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { WaterLogDTO } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WaterService {
  private readonly baseUrl = `${environment.baseUrl}/api/water`;

  constructor(private http: HttpClient) {}

  addWaterLog(userId: string, glasses: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/user/${userId}`, glasses);
  }

  getWaterLogs(userId: string, startDate?: string, endDate?: string): Observable<WaterLogDTO[]> {
    const params: any = {};
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;

    return this.http.get<WaterLogDTO[]>(`${this.baseUrl}/user/${userId}`, { params });
  }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { StepLogDTO } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class StepService {
  private readonly baseUrl = `${environment.baseUrl}/api/steps`;

  constructor(private http: HttpClient) {}

  addStepLog(userId: string, steps: number): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/user/${userId}`, steps);
  }

  getStepLogs(userId: string, startDate?: string, endDate?: string): Observable<StepLogDTO[]> {
    const params: any = {};
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;

    return this.http.get<StepLogDTO[]>(`${this.baseUrl}/user/${userId}`, { params });
  }
}

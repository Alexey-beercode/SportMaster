import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { ActionHistoryDto } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ActionHistoryService {
  private readonly baseUrl = `${environment.baseUrl}/api/action-history`;

  constructor(private http: HttpClient) {}

  getUserActionHistory(
    userId: string,
    startDate?: string,
    endDate?: string
  ): Observable<ActionHistoryDto[]> {
    const params: any = {};
    if (startDate) params.startDate = startDate;
    if (endDate) params.endDate = endDate;

    return this.http.get<ActionHistoryDto[]>(`${this.baseUrl}/user/${userId}`, { params });
  }
}

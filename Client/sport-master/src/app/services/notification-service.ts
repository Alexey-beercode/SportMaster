import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { NotificationDto } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  private readonly baseUrl = `${environment.baseUrl}/api/notifications`;

  constructor(private http: HttpClient) {}

  getNotifications(userId: string): Observable<NotificationDto[]> {
    return this.http.get<NotificationDto[]>(`${this.baseUrl}/user/${userId}`);
  }

  markAsRead(notificationId: string): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/mark-as-read/${notificationId}`, {});
  }
}

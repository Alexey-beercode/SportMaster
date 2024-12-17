import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { RecommendationResponseDTO } from '../models/dtos-response';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RecommendationService {
  private readonly baseUrl = `${environment.baseUrl}/api/recommendations`;

  constructor(private http: HttpClient) {}

  getRecommendations(userId: string): Observable<RecommendationResponseDTO> {
    return this.http.get<RecommendationResponseDTO>(`${this.baseUrl}/user/${userId}`);
  }
}

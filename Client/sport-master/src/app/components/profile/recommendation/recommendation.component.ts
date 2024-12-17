import { Component, Input, OnInit } from '@angular/core';
import { RecommendationService } from '../../../services/recommendation-service';
import { RecommendationResponseDTO } from '../../../models/dtos-response';
import {NgIf} from "@angular/common";

@Component({
  selector: 'app-recommendation',
  standalone: true,
  templateUrl: './recommendation.component.html',
  styleUrls: ['./recommendation.component.css'],
  imports: [
    NgIf
  ]
})
export class RecommendationComponent implements OnInit {
  @Input() userId: string = ''; // ID пользователя
  recommendation: RecommendationResponseDTO | null = null; // Рекомендация пользователя
  errorMessage: string | null = null;

  constructor(private recommendationService: RecommendationService) {}

  ngOnInit(): void {
    this.loadRecommendation();
  }

  loadRecommendation(): void {
    this.recommendationService.getRecommendations(this.userId).subscribe({
      next: (data: RecommendationResponseDTO) => {
        this.recommendation = data;
      },
      error: (err) => {
        this.errorMessage = 'Не удалось загрузить рекомендацию';
        console.error('Ошибка при получении рекомендации:', err);
      },
    });
  }
}

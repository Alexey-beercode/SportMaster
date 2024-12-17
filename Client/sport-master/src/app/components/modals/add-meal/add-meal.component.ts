import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FoodService } from '../../../services/food-service';
import { EnumService } from '../../../services/enum-service';
import { MealType } from '../../../models/enums';
import { FoodLogRequestDTO } from '../../../models/dtos-request';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-add-meal',
  standalone: true,
  templateUrl: './add-meal.component.html',
  styleUrls: ['./add-meal.component.css'],
  imports: [
    FormsModule,
    NgForOf
  ]
})
export class AddMealComponent implements OnInit {
  @Input() userId: string = '';
  @Output() mealAdded = new EventEmitter<void>();
  @Output() close = new EventEmitter<void>();

  mealTypes: { value: string; label: string }[] = [];
  mealType: MealType | null = null;
  foodName: string = '';
  calories: number | null = null;
  protein: number | null = null;
  carbs: number | null = null;
  fat: number | null = null;

  constructor(
    private foodService: FoodService,
    private enumService: EnumService
  ) {
  }

  ngOnInit(): void {
    this.mealTypes = this.enumService.getEnumValues('MealType'); // Инициализация после выполнения конструктора
  }

  addMeal(): void {
    if (!this.mealType || !this.foodName || !this.calories) return;

    const mealRequest: FoodLogRequestDTO = {
      userId: this.userId,
      date: new Date().toISOString(),
      mealType: this.mealType,
      foodName: this.foodName,
      calories: this.calories,
      protein: this.protein || 0,
      carbs: this.carbs || 0,
      fat: this.fat || 0,
    };

    this.foodService.addFoodLog(mealRequest).subscribe({
      next: () => {
        this.mealAdded.emit();
      },
      error: (err) => console.error('Ошибка при добавлении приёма пищи', err),
    });
  }
}





import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoalService } from '../../../services/goal-service';
import { EnumService } from '../../../services/enum-service';
import { GoalType } from '../../../models/enums';
import { GoalRequestDTO } from '../../../models/dtos-request';
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-add-goal',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, NgForOf],
  templateUrl: './add-goal.component.html',
  styleUrls: ['./add-goal.component.css'],
})
export class AddGoalComponent implements OnInit {
  @Input() userId: string = ''; // ID пользователя
  @Output() goalAdded = new EventEmitter<void>(); // Событие после успешного добавления
  @Output() close = new EventEmitter<void>(); // Закрытие модалки

  addGoalForm!: FormGroup; // Форма для добавления цели
  goalTypes: { value: string; label: string }[] = []; // Опции типов целей
  selectedGoalType: GoalType | null = null; // Выбранный тип цели

  constructor(
    private fb: FormBuilder,
    private goalService: GoalService,
    private enumService: EnumService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.goalTypes = this.enumService.getEnumValues('GoalType'); // Загрузка опций целей
  }

  private initForm(): void {
    this.addGoalForm = this.fb.group({
      goalType: ['', Validators.required],
      dailyCalorieIntake: [null, [Validators.required, Validators.min(500)]],
      dailyCalorieBurn: [null, [Validators.required, Validators.min(100)]],
      targetWeight: [null, Validators.min(30)], // Опционально для целей с весом
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.addGoalForm.get(fieldName);
    return field ? field.invalid && (field.dirty || field.touched) : false;
  }

  onSubmit(): void {
    if (this.addGoalForm.valid) {
      const goalRequest: GoalRequestDTO = {
        userId: this.userId,
        goalType: this.addGoalForm.value.goalType as GoalType,
        dailyCalorieIntake: this.addGoalForm.value.dailyCalorieIntake,
        dailyCalorieBurn: this.addGoalForm.value.dailyCalorieBurn,
        targetWeight: this.addGoalForm.value.targetWeight || undefined,
      };

      this.goalService.addGoal(goalRequest).subscribe({
        next: () => {
          this.goalAdded.emit(); // Сообщаем о добавлении
          this.close.emit(); // Закрываем модалку
        },
        error: (err) => console.error('Ошибка при добавлении цели', err),
      });
    } else {
      // Помечаем все поля как touched, чтобы показать ошибки
      Object.keys(this.addGoalForm.controls).forEach((key) => {
        const control = this.addGoalForm.get(key);
        if (control) control.markAsTouched();
      });
    }
  }
}

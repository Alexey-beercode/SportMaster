import { ActionType, GoalType, Gender, MealType, NotificationType } from "./enums";

export interface ActionHistoryDto {
  id: string; // GUID
  userId: string; // GUID
  actionType: ActionType; // Enum (строка или число)
  actionDate: string; // ISO формат
  description: string;
}

export interface GoalWithProgressDTO {
  goalId: string; // GUID
  goalType: GoalType; // Enum
  targetWeight?: number; // Опциональный
  dailyCalorieIntake: number;
  dailyCalorieBurn: number;
  createdDate: string; // ISO format
  progresses: ProgressDto[]; // Список прогрессов
}


export interface AuthResponseDTO {
  accessToken: string;
  userId: string; // GUID
}

export interface CustomGoalDto {
  id: string; // GUID
  userId: string; // GUID
  goalName: string;
  targetValue: number;
  currentValue: number;
  createdDate: string; // ISO формат
}

export interface ExerciseLogDto {
  id: string; // GUID
  userId: string; // GUID
  date: string; // ISO формат
  exerciseType: string;
  duration: number; // minutes
  caloriesBurned: number;
}

export interface FoodLogDto {
  id: string; // GUID
  userId: string; // GUID
  date: string; // ISO формат
  mealType: MealType; // Enum (строка или число)
  foodName: string;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
}

export interface GoalDto {
  id: string; // GUID
  userId: string; // GUID
  goalType: GoalType; // Enum (строка или число)
  targetWeight?: number;
  dailyCalorieIntake: number;
  dailyCalorieBurn: number;
  createdDate: string; // ISO формат
}

export interface NotificationDto {
  id: string; // GUID
  userId: string; // GUID
  message: string;
  type: NotificationType; // Enum (строка или число)
  date: string; // ISO формат
  isRead: boolean;
}

export interface ProgressDto {
  id: string; // GUID
  userId: string; // GUID
  date: string; // ISO формат
  weight: number;
  caloriesConsumed: number;
  caloriesBurned: number;
}

export interface RecommendationResponseDTO {
  recommendationText: string;
}

export interface StepLogDTO {
  stepsCount: number;
  date: string; // ISO формат
}

export interface UserCaloriesDTO {
  caloriesNorm: number;
  caloriesConsumed: number;
  caloriesBurned: number;
}

export interface UserDto {
  username: string;
  email: string;
  passwordHash: string; // Будьте осторожны с хранением паролей
  age: number;
  height: number;
  weight: number;
  gender: Gender; // Enum (число или строка)
  activityLevel: string; // Коэффициент активности
  dailyStepGoal: number; // Норма шагов за день
  dailyWaterGoal: number; // Норма воды (в стаканах)
}

export interface WaterLogDTO {
  glassesDrunk: number;
  date: string; // ISO формат
}

import {ActionType, GoalType, MealType} from "./enums";

export interface LoginDTO {
  email: string;
  password: string;
}

export interface ActionHistoryRequestDTO {
  userId: string;
  actionType: ActionType; // Enum
  actionDate: string; // ISO формат
  description: string;
}

export interface CreateCustomGoalRequestDTO {
  userId: string;
  goalName: string;
  targetValue: number;
  currentValue: number;
}

export interface CreateGoalRequestDTO {
  goalType: GoalType; // Enum
  targetWeight?: number;
  dailyCalorieIntake: number;
  dailyCalorieBurn: number;
  userId: string;
}

export interface ExerciseLogRequestDTO {
  userId: string;
  date: string; // ISO формат
  exerciseType: string;
  duration: number;
  caloriesBurned: number;
}

export interface FoodLogRequestDTO {
  userId: string;
  date: string; // ISO формат
  mealType: MealType; // Enum
  foodName: string;
  calories: number;
  protein: number;
  carbs: number;
  fat: number;
}
export interface GoalRequestDTO {
  userId: string;
  goalType: GoalType; // Enum
  targetWeight?: number;
  dailyCalorieIntake: number;
  dailyCalorieBurn: number;
}

export interface NotificationRequestDTO {
  userId: string;
  message: string;
  type: string; // Можно указать NotificationType, если хотите использовать Enum
  date: string; // ISO формат
}

export interface ProgressRequestDTO {
  userId: string;
  date: string; // ISO формат
  weight: number;
  caloriesConsumed: number;
  caloriesBurned: number;
}

export interface RecommendationRequestDTO {
  userId: string;
  recommendationText: string;
  date: string; // ISO формат
}

export interface UpdateUserRequestDTO {
  age: number;
  height: number;
  weight: number;
  dailyStepGoal: number; // Норма шагов за день
  dailyWaterGoal: number; // Норма воды в мл
}


export interface UserRequestDTO {
  username: string;
  email: string;
  age: number;
  height: number;
  weight: number;
  gender: string; // Можно использовать Gender Enum
  password: string;
}


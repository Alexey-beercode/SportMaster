import { Injectable } from '@angular/core';
import { EnumLabels } from '../models/enum-labels';

@Injectable({
  providedIn: 'root',
})
export class EnumService {
  getEnumLabel<T extends keyof typeof EnumLabels>(
    enumType: T,
    value: keyof typeof EnumLabels[T] | number
  ): string {
    const enumSet = EnumLabels[enumType];

    // Если значение строковое, ищем соответствующую метку
    if (typeof value === 'string' && value in enumSet) {
      return enumSet[value as keyof typeof EnumLabels[T]] as string;
    }

    // Для числовых значений или некорректного ключа возвращаем исходное значение
    return value.toString();
  }

  getEnumValues<T extends keyof typeof EnumLabels>(
    enumType: T
  ): { value: keyof typeof EnumLabels[T]; label: string }[] {
    const labels = EnumLabels[enumType];
    return Object.keys(labels).map((key) => ({
      value: key as keyof typeof EnumLabels[T],
      label: labels[key as keyof typeof EnumLabels[T]] as string,
    }));
  }
}

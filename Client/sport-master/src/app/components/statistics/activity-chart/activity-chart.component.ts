import { Component, Input, AfterViewInit } from '@angular/core';
import { ExerciseLogDto } from '../../../models/dtos-response';
import {
  Chart,
  ChartConfiguration,
  registerables, // Импортируем модули Chart.js
} from 'chart.js';

Chart.register(...registerables); // Регистрируем модули

@Component({
  selector: 'app-activity-chart',
  template: `
    <div class="chart-container">
      <h3 class="chart-title">📊 Физическая активность</h3>
      <canvas id="activityChart"></canvas>
    </div>
  `,
  styleUrls: ['./activity-chart.component.css'],
  standalone: true,
})
export class ActivityChartComponent implements AfterViewInit {
  @Input() exerciseLogs: ExerciseLogDto[] = [];

  ngAfterViewInit(): void {
    this.renderChart();
  }

  renderChart(): void {
    const canvas = document.getElementById('activityChart') as HTMLCanvasElement;
    const ctx = canvas?.getContext('2d');

    if (ctx) {
      // Шаг 1: Агрегация данных по дате
      const aggregatedData = this.aggregateCaloriesByDate(this.exerciseLogs);

      // Получаем даты и суммы калорий
      const labels = Object.keys(aggregatedData);
      const data = Object.values(aggregatedData);

      // Шаг 2: Конфигурация графика
      const chartConfig: ChartConfiguration = {
        type: 'bar',
        data: {
          labels, // Даты
          datasets: [
            {
              label: 'Калории сожжены',
              data, // Сумма калорий по каждой дате
              backgroundColor: 'rgba(76, 175, 80, 0.7)',
              borderColor: '#388e3c',
              borderWidth: 1,
              barThickness: 40, // Ширина столбцов
            },
          ],
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              display: true,
              position: 'top',
              labels: {
                color: '#333333',
                font: { size: 12, weight: 'bold' },
              },
            },
          },
          scales: {
            x: {
              grid: { display: false },
              ticks: { color: '#333333', font: { size: 12 } },
            },
            y: {
              beginAtZero: true,
              ticks: { color: '#333333', font: { size: 12 } },
            },
          },
        },
      };

      // Шаг 3: Создание графика
      new Chart(ctx, chartConfig);
    } else {
      console.error('Canvas context is null. Chart cannot be rendered.');
    }
  }

  // Метод для группировки и суммирования калорий по датам
  private aggregateCaloriesByDate(logs: ExerciseLogDto[]): { [date: string]: number } {
    const aggregatedData: { [date: string]: number } = {};

    logs.forEach((log) => {
      const dateKey = new Date(log.date).toLocaleDateString('ru-RU'); // Форматируем дату
      if (aggregatedData[dateKey]) {
        aggregatedData[dateKey] += log.caloriesBurned; // Суммируем калории
      } else {
        aggregatedData[dateKey] = log.caloriesBurned; // Инициализируем
      }
    });

    return aggregatedData;
  }
}

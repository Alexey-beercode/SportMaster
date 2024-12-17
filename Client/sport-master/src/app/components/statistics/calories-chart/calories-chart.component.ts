import { Component, Input, AfterViewInit } from '@angular/core';
import { FoodLogDto, ExerciseLogDto } from '../../../models/dtos-response';
import {
  Chart,
  ChartConfiguration,
  registerables, // Импортируем модули Chart.js
} from 'chart.js';

Chart.register(...registerables); // Регистрируем модули

@Component({
  selector: 'app-calories-chart',
  template: `
    <div class="chart-container">
      <h3 class="chart-title">🍎 Калории: потребление и сжигание</h3>
      <canvas id="caloriesChart"></canvas>
    </div>
  `,
  styleUrls: ['./calories-chart.component.css'],
  standalone: true,
})
export class CaloriesChartComponent implements AfterViewInit {
  @Input() foodLogs: FoodLogDto[] = [];
  @Input() exerciseLogs: ExerciseLogDto[] = [];

  ngAfterViewInit(): void {
    this.renderChart();
  }

  renderChart(): void {
    const canvas = document.getElementById('caloriesChart') as HTMLCanvasElement;
    const ctx = canvas?.getContext('2d');

    if (ctx) {
      const uniqueDates = this.getUniqueDates();
      const consumedData = uniqueDates.map((date) =>
        this.foodLogs
          .filter((log) => new Date(log.date).toLocaleDateString('ru-RU') === date)
          .reduce((sum, log) => sum + log.calories, 0)
      );
      const burnedData = uniqueDates.map((date) =>
        this.exerciseLogs
          .filter((log) => new Date(log.date).toLocaleDateString('ru-RU') === date)
          .reduce((sum, log) => sum + log.caloriesBurned, 0)
      );

      const chartConfig: ChartConfiguration = {
        type: 'line',
        data: {
          labels: uniqueDates,
          datasets: [
            {
              label: 'Потреблено калорий',
              data: consumedData,
              backgroundColor: 'rgba(255, 152, 0, 0.3)',
              borderColor: '#ff9800',
              borderWidth: 2,
              pointBackgroundColor: '#ff9800',
              fill: true,
              tension: 0.3,
            },
            {
              label: 'Сожжено калорий',
              data: burnedData,
              backgroundColor: 'rgba(76, 175, 80, 0.3)',
              borderColor: '#4caf50',
              borderWidth: 2,
              pointBackgroundColor: '#4caf50',
              fill: true,
              tension: 0.3,
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

      new Chart(ctx, chartConfig);
    } else {
      console.error('Canvas context is null. Chart cannot be rendered.');
    }
  }

  private getUniqueDates(): string[] {
    const dates = [
      ...this.foodLogs.map((log) => new Date(log.date).toLocaleDateString('ru-RU')),
      ...this.exerciseLogs.map((log) => new Date(log.date).toLocaleDateString('ru-RU')),
    ];
    return Array.from(new Set(dates)).sort(
      (a, b) => new Date(a).getTime() - new Date(b).getTime()
    );
  }
}

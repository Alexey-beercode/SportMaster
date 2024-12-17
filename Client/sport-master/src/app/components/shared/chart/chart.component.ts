import { Component, Input, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Chart, ChartType, ChartData, ChartOptions } from 'chart.js';

@Component({
  selector: 'app-chart',
  standalone: true,
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css'],
})
export class ChartComponent implements OnInit {
  @Input() title: string = '';
  @Input() chartType: ChartType = 'bar';
  @Input() chartData: ChartData = { labels: [], datasets: [] };
  @Input() chartOptions: ChartOptions = {
    responsive: true,
    plugins: {
      legend: { display: true },
    },
  };

  @ViewChild('chartCanvas', { static: true }) chartCanvas!: ElementRef;

  private chartInstance!: Chart;

  ngOnInit(): void {
    this.renderChart();
  }

  renderChart(): void {
    if (this.chartInstance) {
      this.chartInstance.destroy();
    }

    this.chartInstance = new Chart(this.chartCanvas.nativeElement, {
      type: this.chartType,
      data: this.chartData,
      options: this.chartOptions,
    });
  }
}

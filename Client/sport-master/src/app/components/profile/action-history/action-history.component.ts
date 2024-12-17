import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { ActionHistoryService } from '../../../services/action-history-service';
import { EnumService } from '../../../services/enum-service';
import { ActionHistoryDto } from '../../../models/dtos-response';
import { ActionType } from '../../../models/enums';
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-action-history-modal',
  standalone: true,
  templateUrl: './action-history.component.html',
  styleUrls: ['./action-history.component.css'],
  imports: [
    NgForOf,
    NgIf
  ]
})
export class ActionHistoryModalComponent implements OnInit {
  @Input() userId: string = ''; // ID пользователя
  @Output() close = new EventEmitter<void>(); // Событие закрытия модалки

  actionHistory: ActionHistoryDto[] = [];

  constructor(
    private actionHistoryService: ActionHistoryService,
    private enumService: EnumService
  ) {}

  ngOnInit(): void {
    this.loadActionHistory();
  }

  loadActionHistory(): void {
    this.actionHistoryService.getUserActionHistory(this.userId).subscribe({
      next: (data) => (this.actionHistory = data),
      error: (err) => console.error('Ошибка при загрузке истории действий', err),
    });
  }

  getActionLabel(actionType: ActionType): string {
    return this.enumService.getEnumLabel('ActionType', actionType);
  }

  formatDate(dateStr: string): string {
    const date = new Date(dateStr);
    return date.toLocaleString('ru-RU', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  }

  closeModal(): void {
    this.close.emit();
  }
}

export interface Notification {
  id: string; // Уникальный идентификатор
  message: string; // Текст уведомления
  date: string; // Дата создания
  isRead: boolean; // Прочитано/непрочитано
}

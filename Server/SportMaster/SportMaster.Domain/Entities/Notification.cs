using SportMaster.Domain.Enums;
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; } = false;
}
namespace SportMaster.BLL.Dtos;

public class NotificationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
}

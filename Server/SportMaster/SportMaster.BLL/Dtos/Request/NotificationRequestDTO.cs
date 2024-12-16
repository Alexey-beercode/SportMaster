namespace SportMaster.BLL.Dtos.Request;

public class NotificationRequestDTO
{
    public Guid UserId { get; set; }
    public string Message { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
}

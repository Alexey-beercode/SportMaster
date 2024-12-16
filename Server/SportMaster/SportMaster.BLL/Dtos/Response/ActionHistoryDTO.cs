namespace SportMaster.BLL.Dtos;

public class ActionHistoryDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ActionType { get; set; }
    public DateTime ActionDate { get; set; }
    public string Description { get; set; }
}
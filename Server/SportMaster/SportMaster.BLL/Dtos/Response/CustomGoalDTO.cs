namespace SportMaster.BLL.Dtos;

public class CustomGoalDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string GoalName { get; set; }
    public decimal TargetValue { get; set; }
    public decimal CurrentValue { get; set; }
    public DateTime CreatedDate { get; set; }
}
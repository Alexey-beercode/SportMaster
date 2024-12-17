namespace SportMaster.BLL.Dtos.Request;

public class CreateCustomGoalRequestDTO
{
    public Guid UserId { get; set; }
    public string GoalName { get; set; }
    public decimal TargetValue { get; set; }
    public decimal CurrentValue { get; set; }
}

namespace SportMaster.BLL.Dtos.Request;

public class CreateGoalRequestDTO
{
    public string GoalType { get; set; }
    public decimal? TargetWeight { get; set; }
    public decimal DailyCalorieIntake { get; set; }
    public decimal DailyCalorieBurn { get; set; }
    public Guid UserId { get; set; }
}

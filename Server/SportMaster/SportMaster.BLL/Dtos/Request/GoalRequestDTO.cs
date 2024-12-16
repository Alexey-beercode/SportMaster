namespace SportMaster.BLL.Dtos.Request;

public class GoalRequestDTO
{
    public Guid UserId { get; set; }
    public string GoalType { get; set; }
    public decimal? TargetWeight { get; set; }
    public decimal DailyCalorieIntake { get; set; }
    public decimal DailyCalorieBurn { get; set; }
}

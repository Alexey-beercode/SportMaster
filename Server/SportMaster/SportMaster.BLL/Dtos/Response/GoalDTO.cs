namespace SportMaster.BLL.Dtos;

public class GoalDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string GoalType { get; set; }
    public decimal? TargetWeight { get; set; }
    public decimal DailyCalorieIntake { get; set; }
    public decimal DailyCalorieBurn { get; set; }
    public DateTime CreatedDate { get; set; }
}
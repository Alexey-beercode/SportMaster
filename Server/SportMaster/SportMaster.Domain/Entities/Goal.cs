using SportMaster.Domain.Enums;
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class Goal : BaseEntity
{
    public Guid UserId { get; set; }
    public GoalType GoalType { get; set; }
    public decimal? TargetWeight { get; set; }
    public decimal DailyCalorieIntake { get; set; }
    public decimal DailyCalorieBurn { get; set; }
    public DateTime CreatedDate { get; set; }
}
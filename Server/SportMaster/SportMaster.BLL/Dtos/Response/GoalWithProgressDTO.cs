using SportMaster.Domain.Enums;

namespace SportMaster.BLL.Dtos.Response;

public class GoalWithProgressDTO
{
    public Guid GoalId { get; set; }
    public GoalType GoalType { get; set; }
    public decimal? TargetWeight { get; set; }
    public decimal DailyCalorieIntake { get; set; }
    public decimal DailyCalorieBurn { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<ProgressDto> Progresses { get; set; } = new();
}

using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class CustomGoal : BaseEntity
{
    public Guid UserId { get; set; }
    public string GoalName { get; set; }
    public decimal TargetValue { get; set; }
    public decimal CurrentValue { get; set; }
    public DateTime CreatedDate { get; set; }
}
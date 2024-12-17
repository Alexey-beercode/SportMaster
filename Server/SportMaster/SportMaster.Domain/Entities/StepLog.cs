using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class StepLog : BaseEntity
{
    public Guid UserId { get; set; }
    public int StepsCount { get; set; }
    public DateTime Date { get; set; }
}

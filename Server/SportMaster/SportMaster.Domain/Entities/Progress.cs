using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class Progress : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }
    public decimal CaloriesConsumed { get; set; }
    public decimal CaloriesBurned { get; set; }
}
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class Recommendation : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string RecommendationText { get; set; }
}

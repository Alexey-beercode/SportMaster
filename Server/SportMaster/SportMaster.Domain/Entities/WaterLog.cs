using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class WaterLog : BaseEntity
{
    public Guid UserId { get; set; }
    public int GlassesDrunk { get; set; } // Количество выпитых стаканов
    public DateTime Date { get; set; }
}

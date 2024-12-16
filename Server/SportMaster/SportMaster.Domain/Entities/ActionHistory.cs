using SportMaster.Domain.Enums;
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class ActionHistory : BaseEntity
{
    public Guid UserId { get; set; }
    public ActionType ActionType { get; set; }
    public DateTime ActionDate { get; set; }
    public string Description { get; set; }
}
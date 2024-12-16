using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class PersonalData : BaseEntity
{
    public Guid UserId { get; set; }
    public string FieldName { get; set; }
    public string FieldValue { get; set; }
}
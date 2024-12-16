using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class ExerciseLog : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string ExerciseType { get; set; }
    public int Duration { get; set; } // in minutes
    public decimal CaloriesBurned { get; set; }
}
namespace SportMaster.BLL.Dtos;

public class ExerciseLogDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string ExerciseType { get; set; }
    public int Duration { get; set; } // minutes
    public decimal CaloriesBurned { get; set; }
}
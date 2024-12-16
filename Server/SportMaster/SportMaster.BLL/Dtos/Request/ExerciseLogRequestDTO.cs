namespace SportMaster.BLL.Dtos.Request;

public class ExerciseLogRequestDTO
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string ExerciseType { get; set; }
    public int Duration { get; set; } // minutes
    public decimal CaloriesBurned { get; set; }
}

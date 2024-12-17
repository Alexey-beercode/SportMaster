namespace SportMaster.BLL.Dtos.Request;

public class UpdateUserRequestDTO
{
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public int DailyStepGoal { get; set; } // Норма шагов за день
    public int DailyWaterGoal { get; set; } 
}
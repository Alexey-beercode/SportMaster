namespace SportMaster.BLL.Dtos.Request;

public class UserRequestDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string Gender { get; set; }
    public string Password { get; set; }
    public string ActivityLevel { get; set; } // Коэффициент активности
    public int DailyStepGoal { get; set; } // Норма шагов за день
    public int DailyWaterGoal { get; set; } // Норма воды (в стаканах)
}

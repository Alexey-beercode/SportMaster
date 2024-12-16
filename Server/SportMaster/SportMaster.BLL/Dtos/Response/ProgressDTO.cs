namespace SportMaster.BLL.Dtos;

public class ProgressDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }
    public decimal CaloriesConsumed { get; set; }
    public decimal CaloriesBurned { get; set; }
}
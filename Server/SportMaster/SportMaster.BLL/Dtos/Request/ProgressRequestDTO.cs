namespace SportMaster.BLL.Dtos.Request;

public class ProgressRequestDTO
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public decimal Weight { get; set; }
    public decimal CaloriesConsumed { get; set; }
    public decimal CaloriesBurned { get; set; }
}
